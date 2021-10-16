namespace GirModel
{
    public interface Parameter
    {
        // TODO: Differentiate between Parameter / Instance Parameter / ParameterList.
        // Instance parameters type is "Type", regular parameters type is "AnyType".
        public string Name { get; }
        public AnyType AnyType { get; }
        Direction Direction { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
    }
}
