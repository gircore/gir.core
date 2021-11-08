namespace Generator3.Generation.Interface
{
    public class NativeMethodsGenerator
    {
        private readonly Template<NativeMethodsModel> _template;
        private readonly Publisher _publisher;

        public NativeMethodsGenerator(Template<NativeMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Interface @interface)
        {
            try
            {
                var model = new NativeMethodsModel(@interface);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@interface.Namespace.GetCanonicalName(), $"{@interface.Name}.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native interface \"{@interface.Name}\"");
                throw;
            }
        }
    }
}
