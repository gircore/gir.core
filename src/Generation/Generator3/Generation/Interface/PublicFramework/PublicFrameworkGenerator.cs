namespace Generator3.Generation.Interface
{
    public class PublicFrameworkGenerator
    {
        private readonly Template<PublicFrameworkModel> _template;
        private readonly Publisher _publisher;

        public PublicFrameworkGenerator(Template<PublicFrameworkModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Interface @interface)
        {
            try
            {
                var model = new PublicFrameworkModel(@interface);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@interface.Namespace.GetCanonicalName(), $"{@interface.Name}.Framework", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public interface framework \"{@interface.Name}\"");
                throw;
            }
        }
    }
}
