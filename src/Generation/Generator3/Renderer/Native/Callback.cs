namespace Generator3.Renderer.Native
{
    public static class Callback
    {
        public static string Render(this Model.Native.Callback callback)
        {
            return $"public delegate {callback.ReturnType.Render()} {callback.Name}({callback.Parameters.Render()});";
        }   
    }
}
