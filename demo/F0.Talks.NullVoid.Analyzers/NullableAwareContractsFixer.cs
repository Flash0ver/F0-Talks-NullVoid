using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace F0.Talks.NullVoid.Analyzers
{
	[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(NullableAwareContractsFixer))]
	[Shared]
	internal sealed class NullableAwareContractsFixer : CodeFixProvider
	{
		public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(NullableAwareContractsAnalyzer.DiagnosticId);

		public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

		public override async Task RegisterCodeFixesAsync(CodeFixContext context)
		{
			SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

			Debug.Assert(root is not null, $"Document doesn't support providing data: {{ {nameof(Document.SupportsSyntaxTree)} = {context.Document.SupportsSyntaxTree} }}");
			SyntaxNode expression = root.FindNode(context.Span);

			PropertyDeclarationSyntax property = (PropertyDeclarationSyntax)expression;

			Debug.Assert(context.Diagnostics.Length is 1);
			Diagnostic diagnostic = context.Diagnostics[0];

			CodeAction action = CodeAction.Create(Resources.CodeFixTitle, ct => ChangeNullabilityAsync(context.Document, property, ct), diagnostic.Id);
			context.RegisterCodeFix(action, diagnostic);
		}

		private static async Task<Document> ChangeNullabilityAsync(Document document, PropertyDeclarationSyntax oldDeclaration, CancellationToken cancellationToken)
		{
			TypeSyntax newType =
				oldDeclaration.Type is NullableTypeSyntax nullable
					? nullable.ElementType.WithTriviaFrom(nullable)
					: SyntaxFactory.NullableType(oldDeclaration.Type);

			PropertyDeclarationSyntax newDeclaration = oldDeclaration.WithType(newType);

			DocumentEditor documentEditor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);
			documentEditor.ReplaceNode(oldDeclaration, newDeclaration);

			return documentEditor.GetChangedDocument();
		}
	}
}
