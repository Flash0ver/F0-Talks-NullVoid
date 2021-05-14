// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Snippets.Code;

namespace Snippets
{
	public class Program
	{
		static void Main(
			string region = null,
			string session = null,
			string package = null,
			string project = null,
			string[] args = null)
		{
			#region Main
			switch (region)
			{
				case "Null_Reference":
					Null.Reference();
					break;
				case "Null_Value":
					Null.Value();
					break;
				case "NRT_Intro":
					NRT.Caller();
					break;
				case "NRT_Contexts":
					NRT.Contexts(new NRT.Class());
					break;
				case "NRT_Flow":
					NRT.Flow();
					break;
				case "NRT_Generic":
					break;
				case "NOP_Enumerable":
					break;
				case "NOP_Async":
					break;
				case "NOP_CancellationToken":
					break;
				case "Q_A":
					QuestionAndAnswer.AnswerQuestionsAsync();
					break;
			}
			#endregion
		}
	}
}
