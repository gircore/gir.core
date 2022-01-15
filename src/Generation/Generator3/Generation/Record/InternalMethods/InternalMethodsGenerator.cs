using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalMethodsGenerator
    {
        private readonly Template<InternalMethodsModel> _template;
        private readonly Publisher _publisher;

        public InternalMethodsGenerator(Template<InternalMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                if (!record.Constructors.Any()
                    && !record.Methods.Any()
                    && !record.Functions.Any()
                    && record.TypeFunction is null)
                    return;

                var model = new InternalMethodsModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), model.Name, source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal methods for record \"{record.Name}\"");
                throw;
            }
        }
    }
}
