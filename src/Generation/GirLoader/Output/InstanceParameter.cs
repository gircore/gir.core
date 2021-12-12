namespace GirLoader.Output
{
    public partial class InstanceParameter : Parameter
    {
        public TypeReference TypeReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public bool CallerAllocates { get; }
        public string Name { get; }

        public InstanceParameter(string name, TypeReference typeReference, Direction direction, Transfer transfer, bool nullable, bool callerAllocates)
        {
            Name = name;
            TypeReference = typeReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            CallerAllocates = callerAllocates;
        }
    }
}
