namespace Generator3.Rendering.Scriban.Renderers
{
    internal class NativeFunctions : Renderer, Generation.NativeFunctions.Renderer
    {
        public NativeFunctions() : base("native.functions.sbntxt"){ }

        public string Render(Generation.NativeFunctions.Data data)
        {
            return TemplateRenderer.Render(Template, data);
        }
    }
}
