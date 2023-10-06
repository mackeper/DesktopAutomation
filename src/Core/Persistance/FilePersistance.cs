namespace Core.Persistance;

public class FilePersistance : IFilePersistance
{
    private readonly IFilePicker filePicker;
    private readonly ITypedFileFactory fileFactory;

    public FilePersistance(IFilePicker filePicker, ITypedFileFactory fileFactory)
    {
        this.filePicker = filePicker;
        this.fileFactory = fileFactory;
    }

    public async Task<Maybe<T>> Open<T>()
    {
        var maybePath = await filePicker.OpenOne();
        var path = maybePath.ValueOrDefault(string.Empty);

        // TODO: Use match
        return string.IsNullOrEmpty(path)
            ? Maybe<T>.None
            : await Open<T>(path);
    }

    public async Task<Maybe<T>> Open<T>(string path)
    {
        var file = fileFactory.Create<T>(path);
        var content = await file.ReadAllText();
        return content;
    }

    public async Task SaveAs<T>(T data)
    {
        var path = await filePicker.SaveOne();
        path.Match(
            async path => await fileFactory.Create<T>(path).WriteAllText(data),
            () => { });
    }

    public async Task Save<T>(string path, T data) => await fileFactory.Create<T>(path).WriteAllText(data);
}
