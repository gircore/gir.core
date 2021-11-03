namespace Generator3.Generation.Framework
{
    public class NativeExtensionsGenerator
    {
        private readonly Template<NativeExtensionsModel> _template;
        private readonly Publisher _publisher;

        public NativeExtensionsGenerator(Template<NativeExtensionsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, string @namespace)
        {
            var model = new NativeExtensionsModel(@namespace);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, "Extensions", source);
            _publisher.Publish(codeUnit);
        }
    }
}
