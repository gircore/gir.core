using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalDelegatesGenerator
    {
        private readonly Template<InternalDelegatesModel> _template;
        private readonly Publisher _publisher;

        public InternalDelegatesGenerator(Template<InternalDelegatesModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Record record)
        {
            try
            {
                if (!HasCallbackField(record))
                    return;

                var model = new InternalDelegatesModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.Delegates", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal delegates for record \"{record.Name}\"");
                throw;
            }
        }

        private bool HasCallbackField(GirModel.Record record)
        {
            return record.Fields.Any(field => field.AnyTypeOrCallback.IsT1);
        }
    }
}
