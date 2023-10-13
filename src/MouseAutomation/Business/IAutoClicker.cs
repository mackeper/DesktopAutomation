using System;
using System.Threading.Tasks;

namespace MouseAutomation.Business;
internal interface IAutoClicker
{
    public Task Start(TimeSpan interval);

    public void Stop();

    public bool IsRunning { get; }
}
