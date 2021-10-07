namespace GirModel
{
    public interface ReturnValue
    {
        public Type Type { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
    }
}
