namespace Generator3.Rendering.Scriban.Renderers
{
    internal class Enumeration : Renderer, Generation.Enumeration.Renderer
    {
        public Enumeration() : base("enumeration.sbntxt") { }

        public string Render(Generation.Enumeration.Data data)
        {
            return TemplateRenderer.Render(Template, data);
        }
    }
}
