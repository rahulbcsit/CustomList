
using System.Collections; 

public interface ICustomList<T> : IEnumerable<T>
{
      void Add(T item);
      bool Remove(T item);

      void RemoveAt(int item);

      T this[int index] {get; set;}

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

      public T this[int index]
      {
            get { return _items[index];}
            set 
            {
                  _items[index] = value;
            } 
      }

      public void Add(T item)
      {
            _items.Add(item);
      }

      public bool Remove(T item)
      {
            return _items.Remove(item);
      }

      public void RemoveAt(int index)
      {
            if (index < 0 || index >= _items.Count)
            {
                  throw new ArgumentOutOfRangeException(nameof(index), $"{index} is out of range");
            }

            _items.RemoveAt(index);
      }
      
      public IEnumerator<T> GetEnumerator()
      {
             foreach (var item in _items)
             {
                  yield return item;
             }
      }

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 
}
