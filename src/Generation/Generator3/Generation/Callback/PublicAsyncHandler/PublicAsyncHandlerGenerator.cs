namespace Generator3.Generation.Callback
{
    public class PublicAsyncHandlerGenerator
    {
        private readonly Template<PublicAsyncHandlerModel> _template;
        private readonly Publisher _publisher;

        public PublicAsyncHandlerGenerator(Template<PublicAsyncHandlerModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new PublicAsyncHandlerModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), $"{callback.Name}.AsyncHandler", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate async handler for callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
