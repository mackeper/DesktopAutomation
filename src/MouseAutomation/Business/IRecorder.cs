using Core.Model;
using System.Collections.Generic;

namespace MouseAutomation.Business;
internal interface IRecorder
{
    void Start();
    void Stop();
    void Clear();
    bool Remove(int id);
    bool IsRecording { get; }
    IEnumerable<RecordStep> GetRecording();
}
