namespace GirLoader.Output;

public partial class Field
{
    public string Name { get; }
    public TypeReference TypeReference { get; }

    public Callback? Callback { get; }
    public bool Readable { get; }
    public bool Writable { get; }
    public bool Private { get; }
    public bool Introspectable { get; }

    /// <summary>
    /// Creates a new field.
    /// </summary>
    /// <param name="typeReference"></param>
    /// <param name="introspectable"></param>
    /// <param name="typeInformation"></param>
    /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
    /// <param name="readable"></param>
    /// <param name="writable"></param>
    /// <param name="private"></param>
    /// <param name="name"></param>
    public Field(string name, TypeReference typeReference, bool introspectable, bool readable = true, bool writable = false, bool @private = false)
    {
        Name = name;
        TypeReference = typeReference;
        Readable = readable;
        Writable = writable;
        Private = @private;
        Introspectable = introspectable;
    }

    public Field(string name, ResolveableTypeReference resolveableTypeReference, Callback callback, bool introspectable, bool readable = true, bool writable = false, bool @private = false)
        : this(name, resolveableTypeReference, introspectable, readable, writable, @private)
    {
        Callback = callback;
        resolveableTypeReference.ResolveAs(Callback);
    }
}
