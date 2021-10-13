namespace Generator3.Renderer
{
    public interface Renderer<T>
    {
        string Render(T data);
    }
}
