#nullable disable

namespace Snippets.Types
{
	internal sealed class NullObliviousType
	{
		public NullObliviousType()
		{
		}

		public NullObliviousType(int number, string text)
		{
			Number = number;
			Text = text;
		}

		public int Number { get; set; }
		public string Text { get; set; }
	}
}
