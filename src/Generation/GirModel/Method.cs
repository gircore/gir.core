using System.Collections.Generic;

namespace GirModel
{
    public interface Method
    {
        string Name { get; }
        ReturnType ReturnType { get; }
        string CIdentifier { get; }
        InstanceParameter InstanceParameter { get; }
        IEnumerable<Parameter> Parameters { get; }

        bool IsUnref() => Name == "unref";
        bool IsFree() => Name == "free";
    }
}
