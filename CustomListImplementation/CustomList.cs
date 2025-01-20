
public interface ICustomList<T> : IEnumerable<T>
{
      void Add(T item);
      bool Remove(T item);

      void RemoveAt(T item);

      T this[int index] {get; set;}

      int Count {get;} 
}

