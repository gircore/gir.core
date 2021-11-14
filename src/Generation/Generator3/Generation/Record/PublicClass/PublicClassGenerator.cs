namespace Generator3.Generation.Record
{
    public class PublicClassGenerator
    {
        private readonly Template<PublicClassModel> _template;
        private readonly Publisher _publisher;

        public PublicClassGenerator(Template<PublicClassModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new PublicClassModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate class for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
