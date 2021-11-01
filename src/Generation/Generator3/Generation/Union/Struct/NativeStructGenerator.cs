namespace Generator3.Generation.Union
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

        public void Generate(string project, GirModel.Union union)
        {
            var model = new NativeStructModel(union);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, $"{union.Name}.Struct", source);
            _publisher.Publish(codeUnit);
        }
    }
}
