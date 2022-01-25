using Generator3.Converter;

namespace Generator3.Generation.Interface
{
    public class InternalMethodsGenerator
    {
        private readonly Template<InternalMethodsModel> _template;
        private readonly Publisher _publisher;

        public InternalMethodsGenerator(Template<InternalMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Interface @interface)
        {
            try
            {
                var model = new InternalMethodsModel(@interface);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@interface.Namespace.GetCanonicalName(), model.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal interface \"{@interface.Name}\"");
                throw;
            }
        }
    }
}
