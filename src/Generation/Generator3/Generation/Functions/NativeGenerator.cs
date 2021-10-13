using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Functions
{
    public class NativeGenerator
    {
        private readonly Renderer<Model.NativeFunctionsUnit> _renderer;
        private readonly Publisher _publisher;

        public NativeGenerator(Renderer<Model.NativeFunctionsUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, IEnumerable<GirModel.Method> functions)
        {
            var model = new Model.NativeFunctionsUnit(functions);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, "Functions", source);
            _publisher.Publish(codeUnit);
        }
    }
}
