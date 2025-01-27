using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace F0.Talks.NullVoid.Analyzers.Tests.Verifiers;

public static partial class CSharpCodeFixVerifier<TAnalyzer, TCodeFix>
	where TAnalyzer : DiagnosticAnalyzer, new()
	where TCodeFix : CodeFixProvider, new()
{
	public static DiagnosticResult Diagnostic()
		=> CSharpCodeFixVerifier<TAnalyzer, TCodeFix, XUnitVerifier>.Diagnostic();

	public static DiagnosticResult Diagnostic(string diagnosticId)
		=> CSharpCodeFixVerifier<TAnalyzer, TCodeFix, XUnitVerifier>.Diagnostic(diagnosticId);

	public static DiagnosticResult Diagnostic(DiagnosticDescriptor descriptor)
		=> CSharpCodeFixVerifier<TAnalyzer, TCodeFix, XUnitVerifier>.Diagnostic(descriptor);

	public static async Task VerifyAnalyzerAsync(string source, params DiagnosticResult[] expected)
	{
		Test test = new()
		{
			TestCode = source,
		};

		test.ExpectedDiagnostics.AddRange(expected);
		await test.RunAsync(CancellationToken.None);
	}

	public static async Task VerifyCodeFixAsync(string source, string fixedSource)
		=> await VerifyCodeFixAsync(source, DiagnosticResult.EmptyDiagnosticResults, fixedSource);

	public static async Task VerifyCodeFixAsync(string source, DiagnosticResult expected, string fixedSource)
		=> await VerifyCodeFixAsync(source, new[] { expected }, fixedSource);

	public static async Task VerifyCodeFixAsync(string source, DiagnosticResult[] expected, string fixedSource)
	{
		Test test = new()
		{
			TestCode = source,
			FixedCode = fixedSource,
		};

		test.ExpectedDiagnostics.AddRange(expected);
		await test.RunAsync(CancellationToken.None);
	}
}
