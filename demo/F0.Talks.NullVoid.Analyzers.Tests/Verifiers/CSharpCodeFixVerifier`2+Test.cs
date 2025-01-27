using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace F0.Talks.NullVoid.Analyzers.Tests.Verifiers;

public static partial class CSharpCodeFixVerifier<TAnalyzer, TCodeFix>
	where TAnalyzer : DiagnosticAnalyzer, new()
	where TCodeFix : CodeFixProvider, new()
{
	public class Test : CSharpCodeFixTest<TAnalyzer, TCodeFix, XUnitVerifier>
	{
		public Test()
		{
			SolutionTransforms.Add((solution, projectId) =>
			{
				Project? project = solution.GetProject(projectId);
				Debug.Assert(project is not null);

				CompilationOptions? compilationOptions = project.CompilationOptions;
				Debug.Assert(compilationOptions is not null);

				compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
					compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
				solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

				return solution;
			});
		}
	}
}
