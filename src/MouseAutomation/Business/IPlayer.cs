using Core.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal interface IPlayer
{
    public bool IsPlaying { get; }
    public Task Play(IEnumerable<ScriptEvent> recording, int iterations, CancellationToken cancellationToken);
    void Subscribe(Action<ScriptEvent> subscriber);
}