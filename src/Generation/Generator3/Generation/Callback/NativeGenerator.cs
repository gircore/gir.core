namespace Generator3.Generation.Callback
{
    public class NativeGenerator
    {
        private readonly Template<NativeModel> _template;
        private readonly Publisher _publisher;

        public NativeGenerator(Template<NativeModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Callback callback)
        {
            var model = new NativeModel(callback);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, callback.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
