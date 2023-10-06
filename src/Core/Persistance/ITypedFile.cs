namespace Core.Persistance;
public interface ITypedFile<T>
{
    Task<Maybe<T>> ReadAllText();

    Task WriteAllText(T text);
}
