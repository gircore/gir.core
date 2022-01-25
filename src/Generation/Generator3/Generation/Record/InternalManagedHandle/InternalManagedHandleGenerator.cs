using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalManagedHandleGenerator
    {
        private readonly Template<InternalManagedHandleModel> _template;
        private readonly Publisher _publisher;

        public InternalManagedHandleGenerator(Template<InternalManagedHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new InternalManagedHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), model.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal managed handle for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
