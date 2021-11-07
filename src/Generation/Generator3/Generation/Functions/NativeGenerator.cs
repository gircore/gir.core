using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Functions
{
    public class NativeGenerator
    {
        private readonly Template<NativeModel> _template;
        private readonly Publisher _publisher;

        public NativeGenerator(Template<NativeModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(IEnumerable<GirModel.Method> functions)
        {
            try
            {
                if (!functions.Any())
                    return;
                
                var model = new NativeModel(functions);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(functions.First().Namespace.GetCanonicalName(), "Functions", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate native functions.");
                throw;
            }
        }
    }
}
