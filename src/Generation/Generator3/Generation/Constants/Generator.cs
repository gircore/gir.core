using System.Collections.Generic;

namespace Generator3.Generation.Constants
{
    public class Generator
    {
        private readonly Template<Model> _template;
        private readonly Publisher _publisher;

        public Generator(Template<Model> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, IEnumerable<GirModel.Constant> constants)
        {
            var model = new Model(constants);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, "Constants", source);
            _publisher.Publish(codeUnit);
        }
    }
}
