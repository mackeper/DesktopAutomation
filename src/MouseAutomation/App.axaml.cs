using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MouseAutomation.Controls;
using MouseAutomation.ViewModels;
using MouseAutomation.Views;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Win32;
using Win32.Interfaces;
using Win32.Models.KeyboardEvents;
using Win32.Models.MouseEvents;

namespace MouseAutomation;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ClearLogFile("log.txt");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
            IMouse mouse = new Mouse();
            mouse.Subscribe<LeftButtonDownEvent>(msg => log.Debug(msg.ToString()));
            mouse.Subscribe<LeftButtonUpEvent>(msg => log.Debug(msg.ToString()));

            IKeyboard keyboard = new Keyboard();
            keyboard.Subscribe<KeyDownEvent>(msg => log.Debug(msg.ToString()));
            keyboard.Subscribe<KeyUpEvent>(msg => log.Debug(msg.ToString()));

            var recording = new ObservableCollection<RecordStep>();
            var recorder = new Recorder(log, mouse, recording);
            var player = new Player(log, mouse, keyboard);

            var mainViewModel = new MainViewModel(log, recorder, player);
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel,
            };

            desktop.ShutdownRequested += (s, e) => mouse.Dispose();
            log.Debug("App initialized");
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    static void ClearLogFile(string logFilePath)
    {
        try
        {
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);
        }
        catch (Exception)
        {
        }
    }
}
