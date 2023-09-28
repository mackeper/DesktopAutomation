using System.Text.Json;

namespace Core.Persistance;
public class JsonFile<T>
{
    protected readonly File file;
    private readonly JsonSerializer jsonSerializer;

    public JsonFile(File file, JsonSerializer jsonSerializer)
    {
        this.file = file;
        this.jsonSerializer = jsonSerializer;
    }

    public async Task<Maybe<T>> ReadAllText() =>
        (await file.ReadAllText()).Match(
            jsonSerializer.Deserialize<T>,
            () => Maybe<T>.None);

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
