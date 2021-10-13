using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Constants
{
    public class Generator
    {
        private readonly Renderer<Model.ConstantsUnit> _renderer;
        private readonly Publisher _publisher;

        public Generator(Renderer<Model.ConstantsUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, IEnumerable<GirModel.Constant> constants)
        {
            var model = new Model.ConstantsUnit(constants);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, "Constants", source);
            _publisher.Publish(codeUnit);
        }
    }
}
