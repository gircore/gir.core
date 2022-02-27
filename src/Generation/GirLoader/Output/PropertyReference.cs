using System;

namespace GirLoader.Output;

public class PropertyReference
{
    private Property? _property;

    public string Name { get; }

    public PropertyReference(string name)
    {
        Name = name;
    }

    public Property GetProperty()
        => _property ?? throw new Exception($"Property {Name} is not resolved");

    public void ResolveProperty(Property property)
    {
        _property = property;
    }

    public static PropertyReference? Create(string? name)
    {
        return name is null
            ? null
            : new PropertyReference(name);
    }
}
