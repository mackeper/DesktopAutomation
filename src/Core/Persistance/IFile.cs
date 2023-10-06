namespace Core.Persistance;
public interface IFile
{
    Task<Maybe<string>> ReadAllText();

    Task WriteAllText(string text);
}
