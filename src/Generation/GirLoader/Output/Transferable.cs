namespace GirLoader.Output
{
    public interface Transferable
    {
        Transfer Transfer { get; }
        
        Type? Type { get; }
    }
}
