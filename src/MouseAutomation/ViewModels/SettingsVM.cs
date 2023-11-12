using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Model.Settings;
using Core.Persistance;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MouseAutomation.ViewModels;
internal partial class SettingsVM : ObservableObject
{
    private Settings settings;
    private readonly JsonFile<Settings> settingsFile;
    private readonly Action<ColorTheme> setColorTheme;
    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private ColorTheme selectedColorTheme;

    [ObservableProperty]
    private ObservableCollection<ColorTheme> availableColorThemes = new();

    public SettingsVM()
    {
        settings = new Settings();
        Enum.GetValues(typeof(ColorTheme)).OfType<ColorTheme>().ForEach(AvailableColorThemes.Add);
    }

    public SettingsVM(Settings settings, JsonFile<Settings> settingsFile, Action<ColorTheme> setColorTheme)
    {
        this.settings = settings;
        this.settingsFile = settingsFile;
        this.setColorTheme = setColorTheme;
        SelectedColorTheme = settings.ColorTheme;
        Enum.GetValues(typeof(ColorTheme)).OfType<ColorTheme>().ForEach(AvailableColorThemes.Add);

        Task.Run(LoadSettings);
    }

    private async Task LoadSettings()
    {
        var loadedSettings = (await settingsFile.ReadAllText())
            .Match(
                s => s,
                () => settings);
        settings = loadedSettings;
        await Dispatcher.UIThread.InvokeAsync(() => SelectedColorTheme = settings.ColorTheme);
    }

    partial void OnSelectedColorThemeChanged(ColorTheme value)
    {
        setColorTheme(value);
        settings.ColorTheme = value;
    }

    [RelayCommand]
    private async Task Save()
    {
        await settingsFile.WriteAllText(settings);
        IsVisible = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        IsVisible = false;
    }
}