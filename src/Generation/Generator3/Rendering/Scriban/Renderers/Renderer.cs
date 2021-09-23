using Scriban;

namespace Generator3.Rendering.Scriban.Renderers
{
    internal abstract class Renderer
    {
        private readonly string _templateName;
        private Template? _template;
        private TemplateRenderer? _templateRenderer;

        protected Template Template => _template ??= TemplateRessourceLoader.Load(_templateName);
        protected TemplateRenderer TemplateRenderer => _templateRenderer ??= new TemplateRenderer();

        protected Renderer(string templateName)
        {
            _templateName = templateName;
        }
    }
}
