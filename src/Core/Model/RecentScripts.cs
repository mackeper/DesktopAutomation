using System.Collections.ObjectModel;

namespace Core.Model;
public class RecentScripts
{
    const int maxRecentScripts = 10;
    private readonly IList<IScript> scripts;

    public RecentScripts(IList<IScript> scripts)
    {
        this.scripts = scripts;
    }

    public IReadOnlyList<IScript> Scripts => scripts.AsReadOnly();

    public void AddScript(IScript script)
    {
        if (scripts.Contains(script))
        {
            scripts.Remove(script);
        }

        scripts.Insert(0, script);

        if (scripts.Count > maxRecentScripts)
        {
            scripts.RemoveAt(maxRecentScripts);
        }
    }
}
