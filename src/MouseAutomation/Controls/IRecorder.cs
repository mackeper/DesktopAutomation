using System.Collections.Generic;

namespace MouseAutomation.Controls;
internal interface IRecorder
{
    void Start();
    void Stop();
    void Clear();
    bool IsRecording { get; }
    IEnumerable<RecordStep> GetRecording();
}
