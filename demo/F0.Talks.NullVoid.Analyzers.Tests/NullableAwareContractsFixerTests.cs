using System.Threading.Tasks;
using F0.Talks.NullVoid.Analyzers.Tests.Verifiers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace F0.Talks.NullVoid.Analyzers.Tests
{
	public class NullableAwareContractsFixerTests
	{
		[Fact]
		public async Task WithDataContractAttribute_FixWrongAnnotations()
		{
			const string source = @"using System;
using System.Runtime.Serialization;

#nullable enable annotations

[DataContract]
public class Message
{
	public Guid Id { get; set; }
	{|#0:[DataMember(IsRequired = true)]
	public string? RequiredText { get; set; }|}
	{|#1:[DataMember(IsRequired = false)]
	public string OptionalText { get; set; }|}
	{|#2:[DataMember(IsRequired = true)]
	public int? RequiredNumber { get; set; }|}
	{|#3:[DataMember(IsRequired = false)]
	public int OptionalNumber { get; set; }|}
}";

			const string fixedSource = @"using System;
using System.Runtime.Serialization;

#nullable enable annotations

[DataContract]
public class Message
{
	public Guid Id { get; set; }
	[DataMember(IsRequired = true)]
	public string RequiredText { get; set; }
	[DataMember(IsRequired = false)]
	public string? OptionalText { get; set; }
	[DataMember(IsRequired = true)]
	public int RequiredNumber { get; set; }
	[DataMember(IsRequired = false)]
	public int? OptionalNumber { get; set; }
}";

			DiagnosticResult[] expected =
			{
				CreateDiagnostic(0, "RequiredText"),
				CreateDiagnostic(1, "OptionalText"),
				CreateDiagnostic(2, "RequiredNumber"),
				CreateDiagnostic(3, "OptionalNumber"),
			};

			await VerifyAsync(source, expected, fixedSource);
		}

		private static DiagnosticResult CreateDiagnostic(int markupKey, params object[] arguments) =>
			CSharpCodeFixVerifier<NullableAwareContractsAnalyzer, NullableAwareContractsFixer>.Diagnostic(NullableAwareContractsAnalyzer.DiagnosticId)
				.WithSeverity(DiagnosticSeverity.Warning)
				.WithMessageFormat("Serializable '{0}' is annotated ambiguously")
				.WithLocation(markupKey)
				.WithArguments(arguments);

		private static Task VerifyAsync(string source, DiagnosticResult[] expected, string fixedSource)
			=> CSharpCodeFixVerifier<NullableAwareContractsAnalyzer, NullableAwareContractsFixer>.VerifyCodeFixAsync(source, expected, fixedSource);
	}
}
