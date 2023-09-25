using System.Collections;
using System.Collections.Generic;

namespace MouseAutomation.Controls.Model;

public sealed class Recording : IEnumerable<RecordStep>, IList<RecordStep>
{
    readonly IList<RecordStep> recordSteps;

    public Recording(IList<RecordStep> recordingCollection)
    {
        recordSteps = recordingCollection;
    }

    public RecordStep this[int index]
    {
        get => recordSteps[index];
        set => recordSteps[index] = value;
    }

    public int Count => recordSteps.Count;

    public bool IsReadOnly => recordSteps.IsReadOnly;

    public void Add(RecordStep item) => recordSteps.Add(item);
    public void Clear() => recordSteps.Clear();
    public bool Contains(RecordStep item) => recordSteps.Contains(item);
    public void CopyTo(RecordStep[] array, int arrayIndex) => recordSteps.CopyTo(array, arrayIndex);
    public IEnumerator<RecordStep> GetEnumerator() => recordSteps.GetEnumerator();
    public int IndexOf(RecordStep item) => recordSteps.IndexOf(item);
    public void Insert(int index, RecordStep item) => recordSteps.Insert(index, item);
    public bool Remove(RecordStep item) => recordSteps.Remove(item);
    public void RemoveAt(int index) => recordSteps.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => recordSteps.GetEnumerator();
}
