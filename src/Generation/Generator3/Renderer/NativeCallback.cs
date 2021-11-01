namespace Generator3.Renderer
{
    public static class NativeCallback
    {
        public static string Render(this Model.NativeCallback callback)
        {
            return $"public delegate {callback.ReturnType.Render()} {callback.Name}({callback.Parameters.Render()});";
        }   
    }
}
