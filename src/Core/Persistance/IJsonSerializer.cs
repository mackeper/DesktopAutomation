using System.Text.Json;

namespace Core.Persistance;
public interface IJsonSerializer
{
    Maybe<T> Deserialize<T>(string source, JsonSerializerOptions? options);
    string Serialize<T>(T source, JsonSerializerOptions? options);
}