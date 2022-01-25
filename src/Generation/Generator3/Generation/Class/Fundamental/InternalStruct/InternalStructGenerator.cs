using Generator3.Converter;

namespace Generator3.Generation.Class.Fundamental
{
    public class InternalStructGenerator
    {
        private readonly Template<InternalStructModel> _template;
        private readonly Publisher _publisher;

        public InternalStructGenerator(Template<InternalStructModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new InternalStructModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Instance.Struct", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal fundamental class instance struct \"{@class.Name}\"");
                throw;
            }
        }
    }
}
