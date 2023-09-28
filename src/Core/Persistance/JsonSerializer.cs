using System.Text.Json;

namespace Core.Persistance;

public class JsonSerializer
{
    public string Serialize<T>(T source, JsonSerializerOptions? options) =>
        System.Text.Json.JsonSerializer.Serialize(source, options);

    public Maybe<T> Deserialize<T>(string source)
    {
        var result = System.Text.Json.JsonSerializer.Deserialize<T>(source);
        return result is null ? Maybe<T>.None : (Maybe<T>)result;
    }
}
