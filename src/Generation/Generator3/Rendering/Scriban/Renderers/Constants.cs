namespace Generator3.Rendering.Scriban.Renderers
{
    internal class Constants : Renderer, Generation.Constants.Renderer
    {
        public Constants() : base("constants.sbntxt") { }

        public string Render(Generation.Constants.Data data)
        {
            return TemplateRenderer.Render(Template, data);
        }
    }
}
