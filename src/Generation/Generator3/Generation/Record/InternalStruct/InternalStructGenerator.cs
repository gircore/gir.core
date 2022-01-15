using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalStructGenerator
    {
        private readonly Template<InternalStructModel> _template;
        private readonly Publisher _publisher;

        public InternalStructGenerator(Template<InternalStructModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new InternalStructModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), model.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal struct for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
