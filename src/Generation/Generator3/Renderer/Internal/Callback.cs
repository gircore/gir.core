namespace Generator3.Renderer.Internal
{
    public static class Callback
    {
        public static string RenderWithAttributes(this Model.Internal.Callback callback)
        {
            return $"public delegate {callback.ReturnType.Render()} {callback.Name}({callback.Parameters.RenderWithAttributes()});";
        }   
    }
}
