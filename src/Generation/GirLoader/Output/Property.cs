namespace GirLoader.Output;

public partial class Property
{
    public string Name { get; }
    public Transfer Transfer { get; }
    public bool Writeable { get; }
    public bool Readable { get; }
    public bool ConstructOnly { get; }
    public AnyTypeReference AnyTypeReference { get; }
    public bool Introspectable { get; }
    public MethodReference? Getter { get; }
    public MethodReference? Setter { get; }

    public Property(string name, AnyTypeReference anyTypeReference, bool writeable, bool readable, bool constructOnly, Transfer transfer, bool introspectable, MethodReference? getter, MethodReference? setter)
    {
        Name = name;
        AnyTypeReference = anyTypeReference;
        Writeable = writeable;
        Transfer = transfer;
        Readable = readable;
        ConstructOnly = constructOnly;
        Introspectable = introspectable;
        Getter = getter;
        Setter = setter;
    }

    public override string ToString()
    {
        return Name;
    }
}
