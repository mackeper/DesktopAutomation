using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Persistance;

public class JsonSerializer : IJsonSerializer
{
    private readonly JsonConverter[] jsonConverters;

    public JsonSerializer(params JsonConverter[] jsonConverters)
    {
        this.jsonConverters = jsonConverters;
    }

    private JsonSerializerOptions? CreateOptions(JsonSerializerOptions? options)
    {
        options ??= new JsonSerializerOptions();
        options?.Converters.Concat(jsonConverters);
        return options;
    }
    public string Serialize<T>(T source, JsonSerializerOptions? options) =>
        System.Text.Json.JsonSerializer.Serialize(source, CreateOptions(options));

    public Maybe<T> Deserialize<T>(string source, JsonSerializerOptions? options)
    {
        var result = System.Text.Json.JsonSerializer.Deserialize<T>(source, CreateOptions(options));
        return result is null ? Maybe<T>.None : (Maybe<T>)result;
    }
}
