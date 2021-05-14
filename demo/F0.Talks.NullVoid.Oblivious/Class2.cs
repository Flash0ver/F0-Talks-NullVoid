using System;
using System.Threading.Tasks;
// ReSharper disable All

namespace F0.Talks.NullVoid.Oblivious
{
	internal class Class2
	{
		private string field;

		public Class2(string value)
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
