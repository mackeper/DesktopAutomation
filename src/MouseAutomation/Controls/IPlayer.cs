using System.Collections.Generic;
using System.Threading.Tasks;

namespace MouseAutomation.Controls;
internal interface IPlayer
{
    public bool IsPlaying { get; }
    public Task Play(IEnumerable<RecordStep> recording);
    public void Stop();
}
