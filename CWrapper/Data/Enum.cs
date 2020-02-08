using System.Collections.Generic;

namespace CWrapper
{
    public interface Enum
    {
        bool HasFlags { get; }
        string Name { get; }
        
        IEnumerable<EnumField> Fields { get; }
    }

    public interface EnumField
    {
        string Name { get; }
        string Value { get; }
    }
}