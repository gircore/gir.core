namespace Generator3.Generation.Framework
{
    public class InternalExtensionsGenerator
    {
        private readonly Template<InternalExtensionsModel> _template;
        private readonly Publisher _publisher;

        public InternalExtensionsGenerator(Template<InternalExtensionsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, string @namespace)
        {
            try
            {
                var model = new InternalExtensionsModel(@namespace);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(project, "Extensions", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate internal extensions");
                throw;
            }
        }
    }
}
