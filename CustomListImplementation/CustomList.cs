
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface ICustomList<T> : IAsyncEnumerable<T>
{
      Task AddAsync(T item);
      Task<bool> RemoveAsync(T item);

      Task RemoveAtAsync(int index);

      Task<T> GetAsync(int index);
      Task SetAsync(int index, T value);

      // GetAsyncEnumerator is inherited from IAsyncEnumerable<T>
      int Count {get;}
}

public class CustomList<T> : ICustomList<T>
{
      private readonly  List<T> _items;

      public CustomList()
      {
            _items = new List<T>();
      } 

      public int Count => _items.Count;

      public Task<T> GetAsync(int index)
      {
            if (index < 0 || index >= _items.Count)
            {
                  throw new ArgumentOutOfRangeException(nameof(index), $"{index} is out of range");
            }
            return Task.FromResult(_items[index]);
      }

      public Task SetAsync(int index, T value)
      {
            if (index < 0 || index >= _items.Count)
            {
                  throw new ArgumentOutOfRangeException(nameof(index), $"{index} is out of range");
            }
            _items[index] = value;
            return Task.CompletedTask;
      }

      public Task AddAsync(T item)
      {
            _items.Add(item);
            return Task.CompletedTask;
      }

      public Task<bool> RemoveAsync(T item)
      {
            return Task.FromResult(_items.Remove(item));
      }

      public Task RemoveAtAsync(int index)
      {
            if (index < 0 || index >= _items.Count)
            {
                  throw new ArgumentOutOfRangeException(nameof(index), $"{index} is out of range");
            }

            _items.RemoveAt(index);
            return Task.CompletedTask;
      }
      
      public async IAsyncEnumerator<T> GetAsyncEnumerator(System.Threading.CancellationToken cancellationToken = default)
      {
          foreach (var item in _items)
          {
              await Task.Yield();
              cancellationToken.ThrowIfCancellationRequested();
              yield return item;
          }
      }
}
