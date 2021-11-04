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

        public void Generate(string project, GirModel.Record record)
        {
            var model = new ClassModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}", source);
            _publisher.Publish(codeUnit);
        }
    }
}
