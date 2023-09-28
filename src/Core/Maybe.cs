namespace Core;

/// <summary>
/// Typical Maybe type
/// Inspired by: https://github.com/ymassad/MaybeExamples (MIT License)
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Maybe<T>
{
    private readonly T value;

    private readonly bool hasValue;

    private Maybe(T value)
    {
        this.value = value;
        hasValue = true;
    }

    public static implicit operator Maybe<T>(T value) =>
        value is null ? new Maybe<T>() : new Maybe<T>(value);

    public static Maybe<T> Some(T value) => new(value);
    public static Maybe<T> None => new();

    private Maybe<TResult> Map<TResult>(Func<T, TResult> convert) =>
        !hasValue ? new Maybe<TResult>() : (Maybe<TResult>)convert(value);

    public Maybe<TResult> Select<TResult>(Func<T, TResult> convert) => Map(convert);

    public T ValueOrDefault(T defaultValue) => hasValue ? value : defaultValue;

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) =>
        hasValue ? some(value) : none();

    public void Match(Action<T> some, Action none)
    {
        if (hasValue)
            some(value);
        else
            none();
    }
}
