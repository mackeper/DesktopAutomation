using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Core.Model;
using Core.Model.Settings;
using Core.Persistance;
using FriendlyWin32;
using FriendlyWin32.Models;
using MouseAutomation.Business;
using MouseAutomation.Mappers;
using MouseAutomation.ViewModels;
using MouseAutomation.Views;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace MouseAutomation;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.File("log-static.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

        Log.Debug("Initialize app");

        ClearLogFile("log.txt");
        var currentVersion = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
            log.Debug("Initialize app");

            var (settings, settingsFile) = CreateSettings();

            var display = new Display();
            var mouse = new Mouse();
            var keyboard = new Keyboard();

            var recording = new Recording();
            var recorder = new Recorder(log, mouse, keyboard);
            var player = new Player(log);

            var autoClicker = new AutoClicker(log, mouse);

            var headerVM = new HeaderVM(
                () => desktop.Shutdown(),
                () => MinimizeWindow(desktop));
            var settingsVM = new SettingsVM(settings, settingsFile, SetColorTheme);
            var footerVM = new FooterVM(currentVersion.ToString(), mouse, () => settingsVM.IsVisible = true);
            var autoClickerVM = new AutoClickerVM(autoClicker);
            var windowHandlerVM = new WindowHandlerVM(display);
            var jsonSerializer = new JsonSerializer();
            var jsonFileFactory = new JsonFileFactory(jsonSerializer);
            var filePicker = new JsonFilePicker(() => desktop.MainWindow?.StorageProvider);
            var filePersistance = new FilePersistance(filePicker, jsonFileFactory);
            var scriptEventMapper = new ScriptEventMapper(mouse, keyboard);
            var editScriptEventVM = new EditScriptEventVM(VirtualKey.VirtualKeys);

            var recorderVM = new RecorderVM(
                log,
                recorder,
                player,
                scriptEventMapper,
                scriptEventMapper,
                editScriptEventVM,
                filePersistance);

            var mainContentVM = new MainContentVM(recorderVM, autoClickerVM, windowHandlerVM, editScriptEventVM);
            var mainVM = new MainVM(log, headerVM, footerVM, mainContentVM, settingsVM);

            // Shortcuts
            var shortcutHandler = new ShortcutHandler(keyboard);

            // Shortcut record command
            shortcutHandler.RegisterShortcut(
                new Shortcut(VirtualKey.F3, new List<VirtualKey> { VirtualKey.CONTROL }),
                recorderVM.Record);
            shortcutHandler.RegisterShortcut(
                new Shortcut(VirtualKey.F4, new List<VirtualKey> { VirtualKey.CONTROL }),
                recorderVM.Record);

            // Shortcut play command
            var playCommandCancellationSource = new CancellationTokenSource();
            shortcutHandler.RegisterShortcut(
                new Shortcut(VirtualKey.F5, new List<VirtualKey> { VirtualKey.CONTROL }),
                async () => await recorderVM.Play());
            shortcutHandler.RegisterShortcut(
                new Shortcut(VirtualKey.F6, new List<VirtualKey> { VirtualKey.CONTROL }),
                recorderVM.PlayCancel);


            desktop.MainWindow = new MainWindow
            {
                DataContext = mainVM,
            };

            desktop.ShutdownRequested += (s, e) =>
            {
                mouse.Dispose();
                keyboard.Dispose();
            };

            desktop.Exit += (s, e) =>
            {
                log.Debug("App exit");
                Log.CloseAndFlush();
            };

            log.Debug("App initialized");
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new RecorderVM(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private (Settings, JsonFile<Settings>) CreateSettings()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var folderName = "DesktopAutomation";
        var fileName = "settings.json";
        var folderPath = System.IO.Path.Combine(appDataPath, folderName);
        var filePath = System.IO.Path.Combine(folderPath, fileName);

        var jsonSerializer = new JsonSerializer();
        var settingsFile = new JsonFile<Settings>(new File(filePath), jsonSerializer);

        try
        {
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
                Log.Information("Created folder: " + folderPath);
            }

        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to create folder: " + folderPath);
        }

        var settings = new Settings();
        SetColorTheme(settings.ColorTheme);

        return (settings, settingsFile);
    }

    private void SetColorTheme(ColorTheme colorTheme)
    {
        var theme = colorTheme switch
        {
            ColorTheme.Default => Avalonia.Styling.ThemeVariant.Default,
            ColorTheme.Dark => Avalonia.Styling.ThemeVariant.Dark,
            ColorTheme.Light => Avalonia.Styling.ThemeVariant.Light,
            _ => throw new NotImplementedException(),
        };
        RequestedThemeVariant = theme;
    }

    static void MinimizeWindow(IClassicDesktopStyleApplicationLifetime desktop)
    {
        var mainWindow = desktop.MainWindow;
        if (mainWindow is null)
            return;
        mainWindow.WindowState = Avalonia.Controls.WindowState.Minimized;
    }

    static void ClearLogFile(string logFilePath)
    {
        try
        {
            if (System.IO.File.Exists(logFilePath))
                System.IO.File.Delete(logFilePath);
        }
        catch (Exception)
        {
        }
    }
}
