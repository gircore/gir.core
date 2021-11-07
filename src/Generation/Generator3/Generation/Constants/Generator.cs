using System.Collections.Generic;
using System.Linq;

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

        public void Generate(IEnumerable<GirModel.Constant> constants)
        {
            try
            {
                var model = new Model(constants);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(constants.First().Namespace.GetCanonicalName(), "Constants", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate constants");
                throw;
            }
        }
    }
}
