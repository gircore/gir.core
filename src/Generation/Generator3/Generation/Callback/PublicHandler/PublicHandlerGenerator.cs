using Generator3.Converter;

namespace Generator3.Generation.Callback
{
    public class PublicHandlerGenerator
    {
        private readonly Template<PublicHandlerModel> _template;
        private readonly Publisher _publisher;

        public PublicHandlerGenerator(Template<PublicHandlerModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new PublicHandlerModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), $"{callback.Name}.Handler", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate handler for callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
