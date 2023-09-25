using Core.Model;
using System.Text.Json;

namespace Core.Persistance;

internal class RecordingFile : TypedFile<Recording>
{
    public RecordingFile(string path) : base(path)
    {
    }

    public override Recording Read()
    {
        if (File.Exists(path))
            try
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<Recording>(json);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, e.g., log or throw.
                // You can also return a default value or null if the read fails.
                throw ex;
            }
        else
            // Handle the case where the file doesn't exist or is empty.
            // You can return a default value or null if needed.
            return null;
    }

    public override void Write(Recording value)
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            // Handle any exceptions here, e.g., log or throw.
            // You can also implement error handling as needed.
            throw ex;
        }
    }
}
