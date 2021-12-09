using Generator3.Converter;

namespace Generator3.Generation.Class.Standard
{
    public class PublicFrameworkGenerator
    {
        private readonly Template<PublicFrameworkModel> _template;
        private readonly Publisher _publisher;

        public PublicFrameworkGenerator(Template<PublicFrameworkModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new PublicFrameworkModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Framework", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public class framework for \"{@class.Name}\"");
                throw;
            }
        }
    }
}
