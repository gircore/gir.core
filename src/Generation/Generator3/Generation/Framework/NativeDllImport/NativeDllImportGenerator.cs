namespace Generator3.Generation.Framework
{
    public class NativeDllImportGenerator
    {
        private readonly Template<NativeDllImportModel> _template;
        private readonly Publisher _publisher;

        public NativeDllImportGenerator(Template<NativeDllImportModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, string sharedLibrary, string @namespace)
        {
            var model = new NativeDllImportModel(sharedLibrary, @namespace);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, "DllImport", source);
            _publisher.Publish(codeUnit);
        }
    }
}
