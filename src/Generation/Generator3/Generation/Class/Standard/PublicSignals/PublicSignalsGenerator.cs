using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Class.Standard
{
    public class PublicSignalsGenerator
    {
        private readonly Template<PublicSignalsModel> _template;
        private readonly Publisher _publisher;

        public PublicSignalsGenerator(Template<PublicSignalsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                if (!@class.Signals.Any())
                    return;
                
                var model = new PublicSignalsModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Signals", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate public class signals for \"{@class.Name}\"");
                throw;
            }
        }
    }
}
