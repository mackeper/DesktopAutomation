namespace Core.Persistance;

public interface IFilePicker
{
    Task<Maybe<string>> SaveOne();

    Task<Maybe<IEnumerable<string>>> OpenMultiple();

    Task<Maybe<string>> OpenOne();
}