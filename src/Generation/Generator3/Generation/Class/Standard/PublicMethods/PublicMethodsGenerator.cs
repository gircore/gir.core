using System.Linq;

namespace Generator3.Generation.Class.Standard
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

        public void Generate(GirModel.Class @class)
        {
            try
            {
                if (!@class.Methods.Any())
                    return;
                
                var model = new PublicMethodsModel(@class);
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
