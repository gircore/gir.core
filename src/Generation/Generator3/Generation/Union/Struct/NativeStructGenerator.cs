namespace Generator3.Generation.Union
{
    public class NativeStructGenerator
    {
        private readonly Template<NativeStructModel> _template;
        private readonly Publisher _publisher;

        public NativeStructGenerator(Template<NativeStructModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Union union)
        {
            try
            {
                var model = new NativeStructModel(union);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(union.Namespace.GetCanonicalName(), $"{union.Name}.Struct", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native struct for union \"{union.Name}\"");
                throw;
            }
        }
    }
}
