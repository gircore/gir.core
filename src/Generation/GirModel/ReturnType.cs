namespace GirModel
{
    public interface ReturnType
    {
        public Type Type { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
    }
}
