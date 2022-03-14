using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalOwnedHandleGenerator
    {
        private readonly Template<InternalOwnedHandleModel> _template;
        private readonly Publisher _publisher;

        public InternalOwnedHandleGenerator(Template<InternalOwnedHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new InternalOwnedHandleModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), model.OwnedHandleName, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal owned handle for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
