using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets.Code
{
	internal static class QuestionAndAnswer
	{
		public static void AnswerQuestionsAsync()
		{
			_ = AnswerQuestionsAsync(EnumerateQuestionsAsync());

			static async IAsyncEnumerable<string> EnumerateQuestionsAsync()
			{
				await Task.Yield();
				yield return $"null & void";
			}
		}

		#region Q_A
		public static async IAsyncEnumerable<string> AnswerQuestionsAsync(IAsyncEnumerable<string> questions, [EnumeratorCancellation] CancellationToken token = default)
		{
			await foreach (string question in questions.WithCancellation(token))
			{
				string answer = await TryAnswerAsync(question);
				yield return answer;
			}
		}
		#endregion

		public static async ValueTask<string> TryAnswerAsync(string question)
		{
			await Task.Yield();

			return String.Empty;
		}
	}
}
