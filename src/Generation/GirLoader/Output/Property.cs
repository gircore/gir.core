namespace GirLoader.Output
{
    public partial class Property
    {
        public string Name { get; }
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }
        public bool Introspectable { get; }

        public Property(string name, TypeReference typeReference, bool writeable, bool readable, Transfer transfer, bool introspectable)
        {
            Name = name;
            TypeReference = typeReference;
            Writeable = writeable;
            Transfer = transfer;
            Readable = readable;
            Introspectable = introspectable;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
