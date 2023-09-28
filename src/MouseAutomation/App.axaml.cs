using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Core.Model;
using Core.Persistance;
using FriendlyWin32;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models;
using FriendlyWin32.Models.MouseEvents;
using MouseAutomation.Business;
using MouseAutomation.ViewModels;
using MouseAutomation.Views;
using Serilog;
using System;
using System.IO;
using System.Reflection;

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
                .MinimumLevel.Debug()
                .WriteTo.File("log-static.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

        ClearLogFile("log.txt");
        var currentVersion = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
            log.Debug("Initialize app");

            var display = new Display();

            IMouse mouse = new Mouse();
            mouse.Subscribe<LeftButtonDownEvent>(msg => log.Debug(msg.ToString()));
            mouse.Subscribe<LeftButtonUpEvent>(msg => log.Debug(msg.ToString()));

            IKeyboard keyboard = new Keyboard();
            keyboard.Subscribe<KeyDownEvent>(msg => log.Debug(msg.ToString()));
            keyboard.Subscribe<KeyUpEvent>(msg => log.Debug(msg.ToString()));

            var recording = new Recording();
            var recorder = new Recorder(log, mouse);
            var player = new Player(log, mouse, keyboard);

            var autoClicker = new AutoClicker(log, mouse);

            var headerVM = new HeaderVM(
                () => desktop.Shutdown(),
                () => MinimizeWindow(desktop));
            var footerVM = new FooterVM(currentVersion.ToString(), mouse);
            var autoClickerVM = new AutoClickerVM(autoClicker);
            var jsonSerializer = new JsonSerializer();
            var jsonFileFactory = new JsonFileFactory(jsonSerializer);
            var filePicker = new JsonFilePicker(() => desktop.MainWindow?.StorageProvider);
            var filePersistance = new FilePersistance(filePicker, jsonFileFactory);
            var scriptVM = new RecorderVM(log, recorder, player, headerVM, footerVM, autoClickerVM, filePersistance);
            var mainVM = new MainVM(log, headerVM, footerVM, scriptVM, autoClickerVM);

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainVM,
            };

            desktop.ShutdownRequested += (s, e) =>
            {
                mouse.Dispose();
                keyboard.Dispose();
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
