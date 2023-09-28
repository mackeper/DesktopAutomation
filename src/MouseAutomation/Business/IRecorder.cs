using Core.Model;

namespace MouseAutomation.Business;
internal interface IRecorder
{
    void Start();

    Recording Stop();

    bool IsRecording { get; }
}
