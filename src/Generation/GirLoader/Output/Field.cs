namespace GirLoader.Output
{
    public partial class Field : Symbol, AnyType
    {
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
        /// <param name="orignalName"></param>
        public Field(string orignalName, TypeReference typeReference, bool readable = true, bool @private = false) : base(orignalName)
        {
            TypeReference = typeReference;
            Readable = readable;
            Private = @private;
        }

        public Field(string orignalName, ResolveableTypeReference resolveableTypeReference, Callback callback, bool readable = true, bool @private = false)
            : this(orignalName, resolveableTypeReference, readable, @private)
        {
            Callback = callback;
            resolveableTypeReference.ResolveAs(Callback);
        }
    }
}
