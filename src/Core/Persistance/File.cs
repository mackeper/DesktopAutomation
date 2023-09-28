namespace Core.Persistance;


public class File
{
    private readonly string path;

    public File(string path)
    {
        this.path = path;
    }

    public async Task<Maybe<string>> ReadAllText()
    {
        try
        {
            return await System.IO.File.ReadAllTextAsync(path);
        }
        catch (Exception)
        {
            return Maybe<string>.None;
        }
    }

    public async Task WriteAllText(string text)
    {
        try
        {
            await System.IO.File.WriteAllTextAsync(path, text);
        }
        catch (Exception) { }
    }
}
