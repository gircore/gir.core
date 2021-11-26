namespace Generator3.Generation.Union
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

        public void Generate(GirModel.Union union)
        {
            try
            {
                var model = new InternalMethodsModel(union);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(union.Namespace.GetCanonicalName(), $"{union.Name}.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal methods for union \"{union.Name}\"");
                throw;
            }
        }
    }
}
