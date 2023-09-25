using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.Enums;
using MouseAutomation.Controls;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal class Player : IPlayer
{
    private readonly ILogger log;
    private readonly IMouse mouse;
    private readonly IKeyboard keyboard;
    private CancellationTokenSource? cancellationTokenSource;

    public Player(ILogger log, IMouse mouse, IKeyboard keyboard)
    {
        this.log = log;
        this.mouse = mouse;
        this.keyboard = keyboard;
    }
    public bool IsPlaying { get; private set; }

    public async Task Play(IEnumerable<RecordStep> recording)
    {
        if (IsPlaying)
            return;

        log.Debug("Start playing");

        cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        IsPlaying = true;
        foreach (var recordStep in recording)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            await PlayStep(recordStep);
        }
        IsPlaying = false;

        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;

        log.Debug("Stop playing");
    }

    public void Stop() => cancellationTokenSource?.Cancel();

    private async Task PlayStep(RecordStep recordStep)
    {
        await Task.Delay(recordStep.Delay);
        log.Debug("Play step: {RecordStep}", recordStep);
        var x = recordStep.X;
        var y = recordStep.Y;
        switch (recordStep.Type)
        {
            case MouseEventType.MouseMove:
                mouse.MoveAbsolute(x, y);
                break;
            case MouseEventType.LeftButtonDown:
                mouse.LeftButtonDown(x, y);
                break;
            case MouseEventType.LeftButtonUp:
                mouse.LeftButtonUp(x, y);
                break;
            case MouseEventType.RightButtonDown:
                mouse.RightButtonDown(x, y);
                break;
            case MouseEventType.RightButtonUp:
                mouse.RightButtonUp(x, y);
                break;
            case MouseEventType.MiddleButtonDown:
                mouse.MiddleButtonDown(x, y);
                break;
            case MouseEventType.MiddleButtonUp:
                mouse.MiddleButtonUp(x, y);
                break;
            default:
                break;
        }
    }
}
