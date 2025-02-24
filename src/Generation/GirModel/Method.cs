namespace GirModel;

public interface Method : Callable
{
    ComplexType Parent { get; }
    string CIdentifier { get; }
    // Methods always have an instance parameter.
    new InstanceParameter InstanceParameter { get; }
    bool Introspectable { get; }
    Property? GetProperty { get; }
    Property? SetProperty { get; }
    string? Version { get; }

    bool IsUnref() => Name == "unref";
    bool IsFree() => Name == "free";
    bool IsAccessor() => GetProperty is not null || SetProperty is not null;
}
