using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace F0.Talks.NullVoid.WebApiApp.Todo;

public sealed class TodoStorage : IDisposable
{
	private readonly SemaphoreSlim mutex = new(1, 1);
	private readonly Dictionary<long, TodoItem> storage = new();
	private long currentId;

	public Task<TodoItem> CreateAsync(TodoData data, CancellationToken cancellationToken)
	{
		_ = data ?? throw new ArgumentNullException(nameof(data));
		return CreateAsyncCore(data, cancellationToken);

		async Task<TodoItem> CreateAsyncCore(TodoData data, CancellationToken cancellationToken)
		{
			await mutex.WaitAsync(cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				currentId++;
				TodoItem item = new(currentId, data.Title, data.Message, data.IsComplete);
				storage.Add(currentId, item);
				return item;
			}
			finally
			{
				mutex.Release();
			}
		}
	}

	public async Task<TodoItem[]> ReadAsync(CancellationToken cancellationToken)
	{
		await mutex.WaitAsync(cancellationToken);

		try
		{
			return storage.Values.OrderBy(static item => item.Id).ToArray();
		}
		finally
		{
			mutex.Release();
		}
	}

	public async Task<TodoItem?> ReadAsync(long id, CancellationToken cancellationToken)
	{
		await mutex.WaitAsync(cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			_ = storage.TryGetValue(id, out TodoItem? item);
			return item;
		}
		finally
		{
			mutex.Release();
		}
	}

	public async Task<bool> UpdateAsync(long id, TodoData data, CancellationToken cancellationToken)
	{
		await mutex.WaitAsync(cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			return TryUpdate(id, data);
		}
		finally
		{
			mutex.Release();
		}
	}

	public async Task<TodoData?> DeleteAsync(long id, CancellationToken cancellationToken)
	{
		await mutex.WaitAsync(cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			if (!storage.TryGetValue(id, out TodoItem? item))
			{
				return null;
			}

			bool removed = storage.Remove(id);
			Debug.Assert(removed);
			return new TodoData(item.Title, item.Message, item.IsComplete);
		}
		finally
		{
			mutex.Release();
		}
	}

	private bool TryUpdate(long id, TodoData data)
	{
		ref TodoItem item = ref CollectionsMarshal.GetValueRefOrNullRef(storage, id);

		if (Unsafe.IsNullRef(ref item))
		{
			return false;
		}

		item = item with
		{
			Title = data.Title,
			Message = data.Message,
			IsComplete = data.IsComplete,
		};

		return true;
	}

	public void Dispose()
	{
		mutex.Dispose();
	}
}
