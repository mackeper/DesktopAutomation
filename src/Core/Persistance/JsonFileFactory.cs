namespace Core.Persistance;
public class JsonFileFactory : ITypedFileFactory
{
    private readonly IJsonSerializer jsonSerializer;

    public JsonFileFactory(IJsonSerializer jsonSerializer)
    {
        this.jsonSerializer = jsonSerializer;
    }

    public ITypedFile<T> Create<T>(string path) => new JsonFile<T>(new File(path), jsonSerializer);
}
