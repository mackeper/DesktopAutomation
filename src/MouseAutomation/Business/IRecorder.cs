﻿using Core.Model;
using System.Collections.Generic;

namespace MouseAutomation.Business;
internal interface IRecorder
{
    void Start();

    IEnumerable<ScriptEvent> Stop();

    bool IsMouseRecording { get; set; }

    bool IsKeyboardRecording { get; set; }

    bool IsRecording { get; }
}
