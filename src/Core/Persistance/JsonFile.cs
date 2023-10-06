using System.Text.Json;

namespace Core.Persistance;
public class JsonFile<T> : ITypedFile<T>
{
    protected readonly IFile file;
    private readonly IJsonSerializer jsonSerializer;

    public JsonFile(IFile file, IJsonSerializer jsonSerializer)
    {
        this.file = file;
        this.jsonSerializer = jsonSerializer;
    }

    public async Task<Maybe<T>> ReadAllText()
    {
        var options = new JsonSerializerOptions { };
        return (await file.ReadAllText()).Match(
            text => jsonSerializer.Deserialize<T>(text, options),
            () => Maybe<T>.None);
    }

    public async Task WriteAllText(T value)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = jsonSerializer.Serialize<T>(value, options);
        await file.WriteAllText(json);
    }
}
