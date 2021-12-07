using System;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace F0.Talks.NullVoid.Library
{
	public sealed class NullLogger : ILogger
	{
		public static ILogger Instance { get; } = new NullLogger();

		private NullLogger()
		{
		}

		IDisposable ILogger.BeginScope<TState>(TState state)
			=> NullDisposable.Instance;

		bool ILogger.IsEnabled(LogLevel logLevel)
			=> false;

		void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			//no-op
		}
	}

	public sealed class NullDisposable : IDisposable
	{
		public static IDisposable Instance { get; } = new NullDisposable();

		private NullDisposable()
		{
		}

		void IDisposable.Dispose()
		{
			//no-op
		}
	}
}
