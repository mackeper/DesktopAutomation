namespace Core.Persistance;

public interface IFilePersistance
{
    Task<Maybe<T>> Open<T>();
    Task<Maybe<T>> Open<T>(string path);
    Task Save<T>(string path, T data);
    Task SaveAs<T>(T data);
}