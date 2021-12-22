using System.Collections.Generic;

namespace GirModel
{
    public interface Namespace
    {
        string Name { get; }
        string Version { get; }
        string? SharedLibrary { get; }

        IEnumerable<Enumeration> Enumerations { get; }
        IEnumerable<Bitfield> Bitfields { get; }
        IEnumerable<Record> Records { get; }
        IEnumerable<Union> Unions { get; }
        IEnumerable<Callback> Callbacks { get; }
        IEnumerable<Function> Functions { get; }
        IEnumerable<Constant> Constants { get; }
        IEnumerable<Interface> Interfaces { get; }
        IEnumerable<Class> Classes { get; }
    }
}
