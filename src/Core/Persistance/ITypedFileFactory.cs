namespace Core.Persistance;
public interface ITypedFileFactory
{
    public ITypedFile<T> Create<T>(string path);
}
