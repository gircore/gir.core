namespace Generator3.Generation.Callback
{
    public class NativeGenerator
    {
        private readonly Template<NativeModel> _template;
        private readonly Publisher _publisher;

        public NativeGenerator(Template<NativeModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new NativeModel(callback);
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
