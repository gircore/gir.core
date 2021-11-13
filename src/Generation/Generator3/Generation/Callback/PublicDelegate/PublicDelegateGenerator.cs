namespace Generator3.Generation.Callback
{
    public class PublicDelegateGenerator
    {
        private readonly Template<PublicDelegateModel> _template;
        private readonly Publisher _publisher;

        public PublicDelegateGenerator(Template<PublicDelegateModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new PublicDelegateModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), $"{callback.Name}.Delegate", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate delegate for callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
