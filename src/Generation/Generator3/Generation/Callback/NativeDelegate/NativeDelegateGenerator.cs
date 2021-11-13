namespace Generator3.Generation.Callback
{
    public class NativeDelegateGenerator
    {
        private readonly Template<NativeDelegateModel> _template;
        private readonly Publisher _publisher;

        public NativeDelegateGenerator(Template<NativeDelegateModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new NativeDelegateModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), callback.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
