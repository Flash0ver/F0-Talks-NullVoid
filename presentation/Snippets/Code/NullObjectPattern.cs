using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets.Code
{
	internal class NullObjectPattern
	{
		#region NOP_Enumerable
		public static IEnumerable<string> GetEnumerable()
		{
			return null;
		}
		#endregion

		#region NOP_Async
		public static Task GetAsync()
		{
			return null;
		}
		#endregion

		#region NOP_CancellationToken
		public static CancellationToken GetCancellationToken()
		{
			return default;
		}
		#endregion
	}
}
