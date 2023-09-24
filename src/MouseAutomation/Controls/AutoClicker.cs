using FriendlyWin32.Interfaces;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Controls;
internal class AutoClicker : IAutoClicker
{
    private readonly ILogger log;
    private readonly IMouse mouse;
    private CancellationTokenSource? cancellationTokenSource = null;

    private const int delayBeforeStart = 500;

    public AutoClicker(ILogger log, IMouse mouse)
    {
        this.log = log;
        this.mouse = mouse;
    }

    public async Task Start(TimeSpan interval)
    {
        log.Information("Starting auto clicker");
        IsRunning = true;


        cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        await Task.Delay(delayBeforeStart, cancellationToken);
        while (!cancellationToken.IsCancellationRequested)
        {
            mouse.Click();
            await Task.Delay(interval, cancellationToken);
        }

        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;
    }

    public void Stop()
    {
        log.Information("Stopping auto clicker");
        IsRunning = false;
        cancellationTokenSource?.Cancel();
    }

    public bool IsRunning { get; private set; }
}
