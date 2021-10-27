namespace Generator3.Generation.Record
{
    public class NativeFunctionsGenerator
    {
        private readonly Template<NativeFunctionsModel> _template;
        private readonly Publisher _publisher;

        public NativeFunctionsGenerator(Template<NativeFunctionsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Record record)
        {
            var model = new NativeFunctionsModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}.Functions", source);
            _publisher.Publish(codeUnit);
        }
    }
}
