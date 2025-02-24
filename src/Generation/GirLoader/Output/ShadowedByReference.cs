using System;

namespace GirLoader.Output;

public class ShadowedByReference
{
    private Callable? _parentCallable;
    private Callable? _callable;

    public string Name { get; }

    private ShadowedByReference(string name)
    {
        Name = name;
    }

    public Callable GetResolvedCallable()
        => _callable ?? throw new Exception($"Shadowed by reference: {Name} is not resolved");

    public Callable GetParentCallable()
        => _parentCallable ?? throw new Exception($"Shadowed by reference {Name}: Parent callable is not set");

    internal void SetParentCallable(Callable parentCallable)
    {
        _parentCallable = parentCallable;
    }

    public void Resolve(Callable callable)
    {
        _callable = callable;
    }

    public static ShadowedByReference? Create(string? name)
    {
        return name is null
            ? null
            : new ShadowedByReference(name);
    }
}
