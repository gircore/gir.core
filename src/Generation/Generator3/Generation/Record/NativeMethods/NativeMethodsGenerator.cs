namespace Generator3.Generation.Record
{
    public class NativeMethodsGenerator
    {
        private readonly Template<NativeMethodsModel> _template;
        private readonly Publisher _publisher;

        public NativeMethodsGenerator(Template<NativeMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            var model = new NativeMethodsModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.Methods", source);
            _publisher.Publish(codeUnit);
        }
    }
}
