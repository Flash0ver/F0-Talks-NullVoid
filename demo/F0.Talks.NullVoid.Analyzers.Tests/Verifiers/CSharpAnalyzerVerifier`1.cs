using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace F0.Talks.NullVoid.Analyzers.Tests.Verifiers;

internal static partial class CSharpAnalyzerVerifier<TAnalyzer>
	where TAnalyzer : DiagnosticAnalyzer, new()
{
	public static DiagnosticResult Diagnostic()
		=> CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic();

	public static DiagnosticResult Diagnostic(string diagnosticId)
		=> CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic(diagnosticId);

	public static DiagnosticResult Diagnostic(DiagnosticDescriptor descriptor)
		=> CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic(descriptor);

	public static async Task VerifyAnalyzerAsync(string source, params DiagnosticResult[] expected)
	{
		Test test = new()
		{
			TestCode = source,
		};

		test.ExpectedDiagnostics.AddRange(expected);
		await test.RunAsync(CancellationToken.None);
	}
}
