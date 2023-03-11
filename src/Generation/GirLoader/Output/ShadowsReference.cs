using System;

namespace GirLoader.Output;

public class ShadowsReference
{
    private Callable? _parentCallable;
    private Callable? _resolvedCallable;

    public string Name { get; }

    private ShadowsReference(string name)
    {
        Name = name;
    }

    public Callable GetResolvedCallable()
        => _resolvedCallable ?? throw new Exception($"Shadows reference: {Name} is not resolved");

    public Callable GetParentCallable()
        => _parentCallable ?? throw new Exception($"Shadows reference {Name}: Parent callable is not set");

    internal void SetParentCallable(Callable parentCallable)
    {
        _parentCallable = parentCallable;
    }

    public void Resolve(Callable callable)
    {
        _resolvedCallable = callable;
    }

    public static ShadowsReference? Create(string? name)
    {
        return name is null
            ? null
            : new ShadowsReference(name);
    }
}
