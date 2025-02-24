namespace GirModel;

public interface Property
{
    string Name { get; }
    AnyType AnyType { get; }
    Transfer Transfer { get; }
    bool Readable { get; }
    bool Writeable { get; }
    bool Introspectable { get; }
    bool ConstructOnly { get; }
    Method? Getter { get; }
    Method? Setter { get; }
}
