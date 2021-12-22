using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalSafeHandleGenerator
    {
        private readonly Template<InternalSafeHandleModel> _template;
        private readonly Publisher _publisher;

        public InternalSafeHandleGenerator(Template<InternalSafeHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new InternalSafeHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.SafeHandle", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal safe handle for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
