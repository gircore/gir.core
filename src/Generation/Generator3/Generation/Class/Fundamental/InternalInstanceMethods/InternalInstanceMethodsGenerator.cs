namespace Generator3.Generation.Class.Fundamental
{
    public class InternalInstanceMethodsGenerator
    {
        private readonly Template<InternalInstanceMethodsModel> _template;
        private readonly Publisher _publisher;

        public InternalInstanceMethodsGenerator(Template<InternalInstanceMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new InternalInstanceMethodsModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Instance.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate internal fundamental class instance methods \"{@class.Name}\"");
                throw;
            }
        }
    }
}
