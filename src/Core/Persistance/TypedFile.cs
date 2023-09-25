namespace Core.Persistance;
internal abstract class TypedFile<T>
{
    protected readonly string path;

    public TypedFile(string path)
    {
        this.path = path;
    }

    public abstract T Read();

    public abstract void Write(T value);
}
