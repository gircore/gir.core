namespace Generator3.Generation.Callback
{
    public class InternalDelegateGenerator
    {
        private readonly Template<InternalDelegateModel> _template;
        private readonly Publisher _publisher;

        public InternalDelegateGenerator(Template<InternalDelegateModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Callback callback)
        {
            try
            {
                var model = new InternalDelegateModel(callback);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(callback.Namespace.GetCanonicalName(), callback.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal callback \"{callback.Name}\"");
                throw;
            }
        }
    }
}
