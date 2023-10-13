using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;
public interface IScript
{
    Guid Id { get; }

    string Name { get; }

    string FilePath { get; }

    int Version { get; }

    IList<IScriptEvent> Events { get; }
}
