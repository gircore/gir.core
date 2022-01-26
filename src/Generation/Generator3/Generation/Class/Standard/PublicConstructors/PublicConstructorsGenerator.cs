using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Class.Standard
{
    public class PublicConstructorGenerator
    {
        private readonly Template<PublicConstructorsModel> _template;
        private readonly Publisher _publisher;

        public PublicConstructorGenerator(Template<PublicConstructorsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                if (!@class.Constructors.Any())
                    return;

                var model = new PublicConstructorsModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Constructors", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public class constructors for \"{@class.Name}\"");
                throw;
            }
        }
    }
}
