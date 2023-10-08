/// <summary>
/// Performs the specified action on each element of the IEnumerable<T>.
/// </summary>
/// <typeparam name="T">The type of elements in the IEnumerable.</typeparam>
/// <param name="source">The IEnumerable<T> to iterate over.</param>
/// <param name="action">The Action<T> delegate to perform on each element.</param>
/// <returns>The original IEnumerable<T> for method chaining.</returns>
/// <exception cref="ArgumentNullException">Thrown if source is null.</exception>
public static class EnumerableExtensions
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (action == null)
            return source;

        foreach (var item in source)
            action(item);

        return source;
    }
}
