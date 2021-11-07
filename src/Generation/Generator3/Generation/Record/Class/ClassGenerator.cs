namespace Generator3.Generation.Record
{
    public class ClassGenerator
    {
        private readonly Template<ClassModel> _template;
        private readonly Publisher _publisher;

        public ClassGenerator(Template<ClassModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                var model = new ClassModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate class for record \"{record.Name}\"");
            }
        }
    }
}
