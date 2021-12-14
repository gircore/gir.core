using Generator3.Converter;

namespace Generator3.Generation.Enumeration
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

        public void Generate(GirModel.Enumeration enumeration)
        {
            try
            {
                var model = new Model(enumeration);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(enumeration.Namespace.GetCanonicalName(), enumeration.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate enumeration \"{enumeration.Name}\"");
                throw;
            }

        }
    }
}
