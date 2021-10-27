namespace Generator3.Generation.Record
{
    public class NativeStructGenerator
    {
        private readonly Template<NativeStructModel> _template;
        private readonly Publisher _publisher;

        public NativeStructGenerator(Template<NativeStructModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Record record)
        {
            var model = new NativeStructModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}.Struct", source);
            _publisher.Publish(codeUnit);
        }
    }
}
