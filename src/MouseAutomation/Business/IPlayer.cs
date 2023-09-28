﻿using Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal interface IPlayer
{
    public bool IsPlaying { get; }
    public Task Play(IEnumerable<RecordStep> recording, CancellationToken cancellationToken);
    public void Stop();
}
