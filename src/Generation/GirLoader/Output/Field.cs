namespace GirLoader.Output;

public partial class Field
{
    public string Name { get; }
    public AnyTypeReference AnyTypeReference { get; }

    public Callback? Callback { get; }
    public bool Readable { get; }
    public bool Writable { get; }
    public bool Private { get; }
    public bool Introspectable { get; }

    /// <summary>
    /// Creates a new field.
    /// </summary>
    /// <param name="anyTypeReference"></param>
    /// <param name="introspectable"></param>
    /// <param name="typeInformation"></param>
    /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
    /// <param name="readable"></param>
    /// <param name="writable"></param>
    /// <param name="private"></param>
    /// <param name="name"></param>
    public Field(string name, AnyTypeReference anyTypeReference, bool introspectable, bool readable, bool writable, bool @private)
    {
        Name = name;
        AnyTypeReference = anyTypeReference;
        Readable = readable;
        Writable = writable;
        Private = @private;
        Introspectable = introspectable;
    }

    public Field(string name, TypeReference typeReference, Callback callback, bool introspectable, bool readable, bool writable, bool @private)
        : this(name, typeReference, introspectable, readable, writable, @private)
    {
        Callback = callback;
        typeReference.ResolveAs(Callback);
    }
}
