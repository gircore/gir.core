namespace Generator3.Generation.Class.Fundamental
{
    public class NativeInstanceStructGenerator
    {
        private readonly Template<NativeInstanceStructModel> _template;
        private readonly Publisher _publisher;

        public NativeInstanceStructGenerator(Template<NativeInstanceStructModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new NativeInstanceStructModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Instance.Struct", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native fundamental class instance struct \"{@class.Name}\"");
                throw;
            }
        }
    }
}
