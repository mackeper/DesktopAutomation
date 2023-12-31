﻿using FriendlyWin32.Interfaces;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
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

        try
        {
            await Task.Delay(delayBeforeStart, cancellationToken);
        }
        catch (TaskCanceledException) { }

        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                mouse.Click();
                try
                {
                    await Task.Delay(interval, cancellationToken);
                }
                catch (TaskCanceledException) { }
            }

        }, cancellationToken);

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
