using System;
using System.Threading.Tasks;

// Ensure CustomList is in the same namespace or add appropriate using directive
// Assuming CustomList<T> is in the global namespace or a namespace already imported.

class Program
{
    static async Task Main(string[] args)
    {
        var list = new CustomList<int>();

        await list.AddAsync(2);
        await list.AddAsync(5);
        await list.AddAsync(10);

        Console.WriteLine("List after adding items:");
        await foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        await list.RemoveAtAsync(1); // Removes the item '5'

        Console.WriteLine("\nList after removing item at index 1 (item '5'):");
        await foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        // Example of using GetAsync - let's get the item at index 0
        if (list.Count > 0)
        {
            try
            {
                var firstItem = await list.GetAsync(0);
                Console.WriteLine($"\nFirst item using GetAsync(0): {firstItem}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"\nError getting item: {ex.Message}");
            }
        }

        // Example of using SetAsync - let's change the item at index 0
        if (list.Count > 0)
        {
            try
            {
                await list.SetAsync(0, 99);
                Console.WriteLine("\nList after setting item at index 0 to 99:");
                await foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"\nError setting item: {ex.Message}");
            }
        }

        // Example of using RemoveAsync - let's remove the item '99'
        bool removed = await list.RemoveAsync(99);
        if (removed)
        {
            Console.WriteLine("\nList after attempting to remove item 99 (should be removed):");
        }
        else
        {
            Console.WriteLine("\nItem 99 not found or not removed.");
        }
        await foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        // Test removing a non-existent item
        bool removedNonExistent = await list.RemoveAsync(123);
        Console.WriteLine($"\nAttempted to remove non-existent item 123. Removed: {removedNonExistent}");

        // Test GetAsync with an out-of-range index
        try
        {
            Console.WriteLine("\nAttempting to GetAsync with out-of-range index:");
            var item = await list.GetAsync(list.Count); // Should throw
            Console.WriteLine($"Item retrieved (should not happen): {item}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught expected exception: {ex.Message}");
        }
         try
        {
            Console.WriteLine("\nAttempting to SetAsync with out-of-range index:");
            await list.SetAsync(list.Count, 1000); // Should throw
            Console.WriteLine($"Item set (should not happen)");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught expected exception: {ex.Message}");
        }
    }
}
