using System;
using System.Threading.Tasks;

namespace MouseAutomation.Controls;
internal interface IAutoClicker
{
    public Task Start(TimeSpan interval);
    public void Stop();

    public bool IsRunning { get; }
}
