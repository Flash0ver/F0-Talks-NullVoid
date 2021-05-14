using System;
using System.Collections.Generic;
using Snippets.Types;

namespace Snippets.Code
{
	internal static class NRT
	{
		#region NRT_Intro
		public static void Caller()
		{
			Callee(new NullObliviousType());
		}

		public static void Callee(NullObliviousType instance)
		{
			Console.WriteLine(instance.Text.Length);
		}
		#endregion

		#region NRT_Contexts
		public class Class
		{
			public string RequiredText { get; set; }
			public string? OptionalText { get; set; }
		}

		public static void Contexts(Class instance)
		{
			Console.WriteLine(instance.RequiredText.Length);
			Console.WriteLine(instance.OptionalText.Length);
		}
		#endregion

		#region NRT_Flow
		#nullable enable
		public class StaticFlowAnalysis
		{
			public string Text { get; set; }

			public StaticFlowAnalysis() { }
			public StaticFlowAnalysis(string text) => Text = text;
		}

		public static void Flow()
			=> Flow(new StaticFlowAnalysis());

		public static void Flow(StaticFlowAnalysis instance)
		{
			Console.WriteLine(instance.Text.Length);
		}

		public static void Set(StaticFlowAnalysis instance)
		{
			instance.Text = null;
		}
		#endregion
		#nullable restore

		#region NRT_Generic
		#nullable enable
		public static void Generic_NotNull<T>(T value)
			where T : notnull
		{
		}

		public static void Generic_Reference<T>(T? value)
			where T : class
		{
		}

		public static void Generic_Value<T>(T? value)
			where T : struct
		{
		}

		public static void Call()
		{
			Generic_NotNull<string>(string.Empty);
			Generic_Reference<string>(null);
			Generic_Value<int>(null);
		}
		#endregion
		#nullable restore
	}
}
