using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Functions
{
    public class InternalGenerator
    {
        private readonly Template<InternalModel> _template;
        private readonly Publisher _publisher;

        public InternalGenerator(Template<InternalModel> template, Publisher publisher)
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
                
                var model = new InternalModel(functions);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(functions.First().Namespace.GetCanonicalName(), "Functions", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate internal functions.");
                throw;
            }
        }
    }
}
