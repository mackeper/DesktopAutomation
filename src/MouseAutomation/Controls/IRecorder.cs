using System.Collections.Generic;

namespace MouseAutomation.Controls;
internal interface IRecorder
{
    void Play();
    void Pause();
    void Stop();
    void Start();
    void Clear();
    bool IsRecording { get; }
    IEnumerable<RecordStep> GetRecording();
}
