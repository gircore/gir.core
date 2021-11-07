using System.Linq;

namespace Generator3.Generation.Record
{
    public class NativeDelegatesGenerator
    {
        private readonly Template<NativeDelegatesModel> _template;
        private readonly Publisher _publisher;

        public NativeDelegatesGenerator(Template<NativeDelegatesModel> template, Publisher publisher)
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

                var model = new NativeDelegatesModel(record);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.Delegates", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native delegates for record \"{record.Name}\"");
                throw;
            }
        }

        private bool HasCallbackField(GirModel.Record record)
        {
            return record.Fields.Any(field => field.AnyTypeOrCallback.IsT1);
        }
    }
}
