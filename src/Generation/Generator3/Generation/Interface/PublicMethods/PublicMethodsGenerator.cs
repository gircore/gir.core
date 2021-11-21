namespace Generator3.Generation.Interface
{
    public class PublicMethodsGenerator
    {
        private readonly Template<PublicMethodsModel> _template;
        private readonly Publisher _publisher;

        public PublicMethodsGenerator(Template<PublicMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Interface @interface)
        {
            try
            {
                var model = new PublicMethodsModel(@interface);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@interface.Namespace.GetCanonicalName(), $"{@interface.Name}.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public interface methods \"{@interface.Name}\"");
                throw;
            }
        }
    }
}
