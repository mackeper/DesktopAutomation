using Core.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal class Player : IPlayer
{
    private readonly ILogger log;
    private readonly List<Action<ScriptEvent>> playerSubscribers = new();

    public Player(ILogger log)
    {
        this.log = log;
    }

    public bool IsPlaying { get; private set; }

    public async Task Play(
        IEnumerable<ScriptEvent> recording,
        int iterations,
        CancellationToken cancellationToken)
    {
        if (IsPlaying)
            return;

        log.Debug("Start playing");

        IsPlaying = true;
        while (iterations-- > 0)
        {
            if (cancellationToken.IsCancellationRequested)
                break;
            foreach (var scriptEvent in recording)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                scriptEvent.IsExecuting = true;
                playerSubscribers.ForEach(s => s(scriptEvent));
                await scriptEvent.Execute(cancellationToken);
                scriptEvent.IsExecuting = false;
            }
        }
        IsPlaying = false;

        log.Debug("Stop playing");
    }

    public void Subscribe(Action<ScriptEvent> subscriber) =>
        playerSubscribers.Add(subscriber);
}