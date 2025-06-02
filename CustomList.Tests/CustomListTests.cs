using System;
using System.Threading.Tasks;
using Xunit;
// CustomList<T> is in the global namespace, so no specific using needed here for that.
// using System.Linq; // Not used
using System.Collections.Generic; // Required for List<T>

public class CustomListTests
{
    [Fact]
    public async Task AddAsync_AddsItem_CountIncreases()
    {
        // Arrange
        var list = new CustomList<int>();

        // Act
        await list.AddAsync(10);
        await list.AddAsync(20);

        // Assert
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public async Task GetAsync_RetrievesCorrectItem()
    {
        // Arrange
        var list = new CustomList<string>();
        await list.AddAsync("hello");
        await list.AddAsync("world");

        // Act
        var item = await list.GetAsync(1);

        // Assert
        Assert.Equal("world", item);
    }

    [Fact]
    public async Task GetAsync_IndexOutOfRange_ThrowsException()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(1);

        // Act & Assert
        await Assert.ThrowsAsync<System.ArgumentOutOfRangeException>(async () => await list.GetAsync(5));
    }

    [Fact]
    public async Task SetAsync_UpdatesItemAtIndex()
    {
        // Arrange
        var list = new CustomList<double>();
        await list.AddAsync(1.0);
        await list.AddAsync(2.0);

        // Act
        await list.SetAsync(0, 1.5);
        var item = await list.GetAsync(0);

        // Assert
        Assert.Equal(1.5, item);
    }

    [Fact]
    public async Task SetAsync_IndexOutOfRange_ThrowsException()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(1);

        // Act & Assert
        await Assert.ThrowsAsync<System.ArgumentOutOfRangeException>(async () => await list.SetAsync(5, 10));
    }

    [Fact]
    public async Task RemoveAsync_RemovesItem_ReturnsTrue()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(10);
        await list.AddAsync(20);

        // Act
        var result = await list.RemoveAsync(10);

        // Assert
        Assert.True(result);
        Assert.Equal(1, list.Count);
        Assert.Equal(20, await list.GetAsync(0));
    }

    [Fact]
    public async Task RemoveAsync_ItemNotPresent_ReturnsFalse()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(10);

        // Act
        var result = await list.RemoveAsync(30);

        // Assert
        Assert.False(result);
        Assert.Equal(1, list.Count);
    }

    [Fact]
    public async Task RemoveAtAsync_RemovesItemAtIndex()
    {
        // Arrange
        var list = new CustomList<string>();
        await list.AddAsync("a");
        await list.AddAsync("b");
        await list.AddAsync("c");

        // Act
        await list.RemoveAtAsync(1); // Remove "b"

        // Assert
        Assert.Equal(2, list.Count);
        Assert.Equal("a", await list.GetAsync(0));
        Assert.Equal("c", await list.GetAsync(1));
    }

    [Fact]
    public async Task RemoveAtAsync_IndexOutOfRange_ThrowsException()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(1);

        // Act & Assert
        await Assert.ThrowsAsync<System.ArgumentOutOfRangeException>(async () => await list.RemoveAtAsync(5));
    }

    [Fact]
    public async Task GetAsyncEnumerator_IteratesOverAllItems()
    {
        // Arrange
        var list = new CustomList<int>();
        await list.AddAsync(1);
        await list.AddAsync(2);
        await list.AddAsync(3);
        var results = new System.Collections.Generic.List<int>();

        // Act
        await foreach (var item in list)
        {
            results.Add(item);
        }

        // Assert
        Assert.Equal(new[] { 1, 2, 3 }, results);
    }

    [Fact]
    public async Task GetAsyncEnumerator_EmptyList_IteratesCorrectly()
    {
        // Arrange
        var list = new CustomList<int>();
        var results = new System.Collections.Generic.List<int>();

        // Act
        await foreach (var item in list)
        {
            results.Add(item);
        }

        // Assert
        Assert.Empty(results);
    }
}
