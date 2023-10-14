using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;
public class ScriptDTO
{
    public Guid Id { get; }

    public string Name { get; }

    public string FilePath { get; }

    public int Version { get; }

    public IList<ScriptEventDTO> Events { get; }

    public ScriptDTO(Guid id, string name, string filePath, int version, IList<ScriptEventDTO> events)
    {
        Id = id;
        Name = name;
        FilePath = filePath;
        Version = version;
        Events = events;
    }

}
