namespace Generator3.Generation.Class.Standard
{
    public class PublicClassMethodsGenerator
    {
        private readonly Template<PublicClassMethodsModel> _template;
        private readonly Publisher _publisher;

        public PublicClassMethodsGenerator(Template<PublicClassMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new PublicClassMethodsModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public class methods for \"{@class.Name}\"");
                throw;
            }
        }
    }
}
