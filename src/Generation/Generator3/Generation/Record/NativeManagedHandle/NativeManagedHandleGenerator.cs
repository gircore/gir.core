namespace Generator3.Generation.Record
{
    public class NativeManagedHandleGenerator
    {
        private readonly Template<NativeManagedHandleModel> _template;
        private readonly Publisher _publisher;

        public NativeManagedHandleGenerator(Template<NativeManagedHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new NativeManagedHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.ManagedHandle", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native managed handle for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
