using MouseAutomation.Controls.Model;
using System;

namespace MouseAutomation.Controls.Persistance;
internal class RecordingFile : TypedFile<Recording>
{
    public RecordingFile(string path) : base(path)
    {
    }

    public override Recording Read() => throw new NotImplementedException();
    public override void Write(Recording value) => throw new NotImplementedException();
}
