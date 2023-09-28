namespace Core.Persistance;
public class JsonFileFactory
{
    private readonly JsonSerializer jsonSerializer;

    public JsonFileFactory(JsonSerializer jsonSerializer)
    {
        this.jsonSerializer = jsonSerializer;
    }

    public JsonFile<T> Create<T>(string path) => new JsonFile<T>(new File(path), jsonSerializer);

}
