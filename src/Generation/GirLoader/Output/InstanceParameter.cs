namespace GirLoader.Output
{
    public partial class InstanceParameter : Symbol, Parameter
    {
        public TypeReference TypeReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public bool CallerAllocates { get; }
        string Parameter.Name => OriginalName;

        public InstanceParameter(string originalName, TypeReference typeReference, Direction direction, Transfer transfer, bool nullable, bool callerAllocates) : base(originalName)
        {
            TypeReference = typeReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            CallerAllocates = callerAllocates;
        }
    }
}
