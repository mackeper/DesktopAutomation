using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Model.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MouseAutomation.ViewModels;
internal partial class SettingsVM : ObservableObject
{
    private Settings settings;
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

    public SettingsVM(Settings settings, Action<ColorTheme> setColorTheme)
    {
        this.settings = settings;
        this.setColorTheme = setColorTheme;
        SelectedColorTheme = settings.ColorTheme;
        Enum.GetValues(typeof(ColorTheme)).OfType<ColorTheme>().ForEach(AvailableColorThemes.Add);
    }

    partial void OnSelectedColorThemeChanged(ColorTheme value)
        => setColorTheme(value);

    [RelayCommand]
    private void Save()
    {
        IsVisible = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        IsVisible = false;
    }
}
