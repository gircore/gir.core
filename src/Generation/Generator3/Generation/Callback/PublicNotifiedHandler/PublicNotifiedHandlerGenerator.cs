using Generator3.Converter;

namespace Generator3.Generation.Callback
{
    public class PublicNotifiedHandlerGenerator
    {
        private readonly Template<PublicNotifiedHandlerModel> _template;
        private readonly Publisher _publisher;

        public PublicNotifiedHandlerGenerator(Template<PublicNotifiedHandlerModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new PublicNotifiedHandlerModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), $"{callback.Name}.NotifiedHandler", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate async notified for callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
