using System.Linq;

namespace Generator3.Generation.Class.Standard
{
    public class PublicPropertiesGenerator
    {
        private readonly Template<PublicPropertiesModel> _template;
        private readonly Publisher _publisher;

        public PublicPropertiesGenerator(Template<PublicPropertiesModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                if (!@class.Properties.Any())
                    return;
                
                var model = new PublicPropertiesModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Properties", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public class properties for \"{@class.Name}\"");
                throw;
            }
        }
    }
}
