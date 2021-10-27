namespace Generator3.Generation
{
    public interface Template<in T>
    {
        string Render(T data);
    }
}
