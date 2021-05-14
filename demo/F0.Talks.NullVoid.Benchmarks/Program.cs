using BenchmarkDotNet.Running;

namespace F0.Talks.NullVoid.Benchmarks
{
	internal static class Program
	{
		private static void Main(string[] args)
			=> _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
	}
}
