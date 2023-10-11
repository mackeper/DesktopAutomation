using Core.Model;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal class Player : IPlayer
{
    private readonly ILogger log;

    public Player(ILogger log)
    {
        this.log = log;
    }
    public bool IsPlaying { get; private set; }

    public async Task Play(IEnumerable<ScriptEvent> recording, int iterations, CancellationToken cancellationToken)
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

                await scriptEvent.Execute(cancellationToken);
            }
        }
        IsPlaying = false;

        log.Debug("Stop playing");
    }
}
