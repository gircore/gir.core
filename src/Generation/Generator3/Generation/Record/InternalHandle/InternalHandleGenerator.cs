using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalHandleGenerator
    {
        private readonly Template<InternalHandleModel> _template;
        private readonly Publisher _publisher;

        public InternalHandleGenerator(Template<InternalHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new InternalHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), model.Name, source);
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
