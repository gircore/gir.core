using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Enumeration
{
    public class Generator
    {
        private readonly Renderer<Model.EnumerationUnit> _renderer;
        private readonly Publisher _publisher;

        public Generator(Renderer<Model.EnumerationUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Enumeration enumeration)
        {
            var model = new Model.EnumerationUnit(enumeration);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, enumeration.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
