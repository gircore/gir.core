namespace Generator3.Generation.Enumeration
{
    public class Generator
    {
        private readonly Template<Model> _template;
        private readonly Publisher _publisher;

        public Generator(Template<Model> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Enumeration enumeration)
        {
            var model = new Model(enumeration);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, enumeration.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
