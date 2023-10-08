namespace Core.Tests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void ForEach_DoesNotModifySourceList_WhenActionIsApplied()
    {
        // Arrange
        IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var result = new List<int>();
        numbers.ForEach(n => result.Add(n * 2));

        // Assert
        Assert.Equal(new List<int> { 2, 4, 6, 8, 10 }, result);
    }

    [Fact]
    public void ForEach_ReturnsIEnumerable_ForMethodChaining()
    {
        // Arrange
        IEnumerable<string> strings = new List<string> { "one", "two", "three" };

        // Act
        var result = strings.ForEach(n => n += n);

        // Assert
        Assert.Same(strings, result);
    }

    [Fact]
    public void ForEach_ThrowsArgumentNullException_WhenSourceIsNull()
    {
        // Arrange
        IEnumerable<int> numbers = null!;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => numbers.ForEach(n => Console.WriteLine(n)));
    }
}
