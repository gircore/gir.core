namespace GirLoader.Output
{
    public partial class Field : AnyType
    {
        public string Name { get; }
        public TypeReference TypeReference { get; }

        public Callback? Callback { get; }
        public bool Readable { get; }
        public bool Private { get; }

        /// <summary>
        /// Creates a new field.
        /// </summary>
        /// <param name="typeReference"></param>
        /// <param name="typeInformation"></param>
        /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
        /// <param name="readable"></param>
        /// <param name="private"></param>
        /// <param name="name"></param>
        public Field(string name, TypeReference typeReference, bool readable = true, bool @private = false)
        {
            Name = name;
            TypeReference = typeReference;
            Readable = readable;
            Private = @private;
        }

        public Field(string name, ResolveableTypeReference resolveableTypeReference, Callback callback, bool readable = true, bool @private = false)
            : this(name, resolveableTypeReference, readable, @private)
        {
            Callback = callback;
            resolveableTypeReference.ResolveAs(Callback);
        }
    }
}
