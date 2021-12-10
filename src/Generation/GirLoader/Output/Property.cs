namespace GirLoader.Output
{
    public partial class Property : Symbol, TransferableAnyType
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }

        public Property(string originalName, TypeReference typeReference, bool writeable, bool readable, Transfer transfer) : base(originalName)
        {
            TypeReference = typeReference;
            Writeable = writeable;
            Transfer = transfer;
            Readable = readable;
        }
    }
}
