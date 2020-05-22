using System.Collections.Generic;

namespace Generator
{
    public class Enum
    {
        bool HasFlags { get; }
        string Name { get; }
        
        IEnumerable<EnumField> Fields { get; }

        public Enum(bool hasFlags, string name, IEnumerable<EnumField> fields)
        {
            HasFlags = hasFlags;
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
        }
    }

    public class EnumField
    {
        string Name { get; }
        string Value { get; }

        public EnumField(string name, string value)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }
    }
}