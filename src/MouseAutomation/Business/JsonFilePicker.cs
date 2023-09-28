using Avalonia.Platform.Storage;
using Core;
using Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal class JsonFilePicker : IFilePicker
{
    private readonly Func<IStorageProvider> getStorageProvider;

    public JsonFilePicker(Func<IStorageProvider> getStorageProvider)
    {
        this.getStorageProvider = getStorageProvider;
    }

    public async Task<Maybe<string>> SaveOne()
    {
        var jsonFiletype = new FilePickerFileType("Json")
        {
            Patterns = new[] { "*.json" },
            MimeTypes = new[] { "application/json" },
        };
        var options = new FilePickerSaveOptions()
        {
            Title = "Save file",
            FileTypeChoices = new[] { jsonFiletype },
            DefaultExtension = "json",
        };

        var file = await getStorageProvider().SaveFilePickerAsync(options);
        return file is null
            ? Maybe<string>.None
            : file.Path.AbsolutePath;
    }

    private async Task<IEnumerable<string>> Open(FilePickerOpenOptions options)
    {
        var storageProvider = getStorageProvider();
        var files = await storageProvider.OpenFilePickerAsync(options);
        return files.Select(file => file.Path.AbsolutePath);
    }

    public async Task<Maybe<IEnumerable<string>>> OpenMultiple()
    {
        var jsonFiletype = new FilePickerFileType("Json")
        {
            Patterns = new[] { "*.json" },
            MimeTypes = new[] { "application/json" },
        };
        var options = new FilePickerOpenOptions()
        {
            Title = "Open files",
            AllowMultiple = true,
            FileTypeFilter = new[] { jsonFiletype },
        };

        var files = await Open(options);
        return files.Any()
            ? Maybe<IEnumerable<string>>.Some(files)
            : Maybe<IEnumerable<string>>.None;
    }

    public async Task<Maybe<string>> OpenOne()
    {
        var jsonFiletype = new FilePickerFileType("Json")
        {
            Patterns = new[] { "*.json" },
            MimeTypes = new[] { "application/json" },
        };
        var options = new FilePickerOpenOptions()
        {
            Title = "Open file",
            AllowMultiple = false,
            FileTypeFilter = new[] { jsonFiletype },
        };

        var files = await Open(options);
        return files.Any()
            ? Maybe<string>.Some(files.First())
            : Maybe<string>.None;
    }
}
