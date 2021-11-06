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
            if (!HasCallbackField(record))
                return;

            var model = new NativeDelegatesModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.Delegates", source);
            _publisher.Publish(codeUnit);
        }

        private bool HasCallbackField(GirModel.Record record)
        {
            return record.Fields.Any(field => field.AnyTypeOrCallback.IsT1);
        }
    }
}
