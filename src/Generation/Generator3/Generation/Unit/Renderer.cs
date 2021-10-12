namespace Generator3.Generation.Unit
{
    public interface Renderer<T>
    {
        string Render(T data);
    }
}
