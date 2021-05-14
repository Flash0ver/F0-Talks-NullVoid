using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace F0.Talks.NullVoid.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	internal sealed class NullableAwareContractsAnalyzer : DiagnosticAnalyzer
	{
		internal const string DiagnosticId = "F0Null";

		private static readonly LocalizableString Title =
			new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));

		private static readonly LocalizableString MessageFormat =
			new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));

		private static readonly LocalizableString Description =
			new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));

		private const string Category = "Nullable";

		private static readonly DiagnosticDescriptor Rule = new(
			DiagnosticId,
			Title,
			MessageFormat,
			Category,
			DiagnosticSeverity.Warning,
			true,
			Description,
			"https://github.com/Flash0ver/F0-Talks-NullVoid"
		);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

		public override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
			context.EnableConcurrentExecution();

			context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, DataContractSyntaxKinds);
		}

		private static readonly ImmutableArray<SyntaxKind> DataContractSyntaxKinds = ImmutableArray.Create(
			SyntaxKind.ClassDeclaration,
			SyntaxKind.StructDeclaration,
			SyntaxKind.RecordDeclaration
		);

		private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
		{
			Debug.Assert(context.ContainingSymbol is not null);
			Debug.Assert(context.Node is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax);

			NullableContextOptions nullableContextOptions =
				context.Compilation.Options.NullableContextOptions;
			NullableContext nullableContext =
				context.SemanticModel.GetNullableContext(context.Node.GetLocation().SourceSpan.Start);

			if (!nullableContext.HasFlag(NullableContext.AnnotationsEnabled)
				&& !nullableContextOptions.HasFlag(NullableContextOptions.Annotations))
			{
				return;
			}

			INamedTypeSymbol symbol = (INamedTypeSymbol)context.ContainingSymbol;

			INamedTypeSymbol? dataContractAttribute =
				context.Compilation.GetTypeByMetadataName("System.Runtime.Serialization.DataContractAttribute");

			if (!TryGetAttribute(symbol, dataContractAttribute, out _))
			{
				return;
			}

			ImmutableArray<ISymbol> dataMembers = symbol.GetMembers()
				.Where(member => member.DeclaredAccessibility is Accessibility.Public)
				.Where(member => member.Kind is SymbolKind.Property)
				.ToImmutableArray();

			INamedTypeSymbol? dataMemberAttribute =
				context.Compilation.GetTypeByMetadataName("System.Runtime.Serialization.DataMemberAttribute");

			foreach (ISymbol dataMember in dataMembers)
			{
				if (!TryGetAttribute(dataMember, dataMemberAttribute, out AttributeData? attribute))
				{
					continue;
				}

				IPropertySymbol property = (IPropertySymbol)dataMember;
				NullableAnnotation annotation = property.NullableAnnotation;

				if (annotation is NullableAnnotation.None)
				{
					continue;
				}

				TypedConstant isRequired = attribute.NamedArguments
					.SingleOrDefault(argument => argument.Key.Equals("IsRequired", StringComparison.Ordinal))
					.Value;

				if (isRequired.Kind is TypedConstantKind.Error || isRequired.Value is false)
				{
					if (annotation is NullableAnnotation.NotAnnotated)
					{
						ReportDiagnostic(context, property);
					}
				}
				else
				{
					if (annotation is NullableAnnotation.Annotated)
					{
						ReportDiagnostic(context, property);
					}
				}
			}
		}

		private static bool TryGetAttribute(ISymbol symbol, INamedTypeSymbol? attributeType, [MaybeNullWhen(false)] out AttributeData attribute)
		{
			ImmutableArray<AttributeData> attributes = symbol.GetAttributes()
				.Where(symbolAttribute => IsAttribute(symbolAttribute, attributeType))
				.ToImmutableArray();

			if (attributes.Length is 1)
			{
				attribute = attributes[0];
				return true;
			}
			else
			{
				attribute = null;
				return false;
			}

			static bool IsAttribute(AttributeData attribute, INamedTypeSymbol? attributeType)
			{
				return attribute.AttributeClass is not null
					&& attribute.AttributeClass.Equals(attributeType, SymbolEqualityComparer.Default);
			}
		}

		private static void ReportDiagnostic(SyntaxNodeAnalysisContext context, ISymbol symbol)
		{
			SyntaxReference? syntaxReference = symbol.DeclaringSyntaxReferences.FirstOrDefault();

			if (syntaxReference is not null)
			{
				SyntaxNode syntaxNode = syntaxReference.GetSyntax(context.CancellationToken);

				Diagnostic diagnostic = Diagnostic.Create(Rule, syntaxNode.GetLocation(), symbol.Name);
				context.ReportDiagnostic(diagnostic);
			}
		}
	}
}
