using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace F0.Talks.NullVoid.Analyzers.Tests.Verifiers;

internal static partial class CSharpAnalyzerVerifier<TAnalyzer>
	where TAnalyzer : DiagnosticAnalyzer, new()
{
	internal sealed class Test : CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
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
