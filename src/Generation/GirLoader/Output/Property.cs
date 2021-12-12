namespace GirLoader.Output
{
    public partial class Property : TransferableAnyType
    {
        public string Name { get; }
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }

        public Property(string name, TypeReference typeReference, bool writeable, bool readable, Transfer transfer)
        {
            Name = name;
            TypeReference = typeReference;
            Writeable = writeable;
            Transfer = transfer;
            Readable = readable;
        }
    }
}
