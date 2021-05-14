using System;

namespace Snippets.Code
{
	internal static class Null
	{
		public static void Reference()
		{
			#region Null_Reference
			string text = "Text";
			Console.WriteLine(text.Length);
			#endregion
		}

		public static void Value()
		{
			#region Null_Value
			int number = 240;
			Console.WriteLine(number);
			#endregion
		}
	}
}
