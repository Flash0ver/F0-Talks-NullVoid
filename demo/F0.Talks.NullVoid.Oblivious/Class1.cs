using System;
using System.Threading.Tasks;
// ReSharper disable All

namespace F0.Talks.NullVoid.Oblivious
{
	internal sealed class Class1
	{
		private readonly string field;

		public Class1(string value)
		{
			field = value;
		}

		public string Property { get; private set; }

		public string Method()
		{
			return null;
		}

		public void Method(string value)
		{
			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			Property = value;
		}

		public Task<string> MethodAsync()
		{
			return Task.FromResult<string>(null);
		}
	}
}
