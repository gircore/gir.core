namespace Generator3.Generation.Record
{
    public class NativeSafeHandleGenerator
    {
        private readonly Template<NativeSafeHandleModel> _template;
        private readonly Publisher _publisher;

        public NativeSafeHandleGenerator(Template<NativeSafeHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new NativeSafeHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.SafeHandle", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native safe handle for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
