namespace Generator3.Generation.Record
{
    public class NativeSafeHandleGenerator
    {
        private readonly Template<NativeSafeHandleModel> _template;
        private readonly Publisher _publisher;

        public NativeSafeHandleGenerator(Template<NativeSafeHandleModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Record record)
        {
            var model = new NativeSafeHandleModel(record);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}.SafeHandle", source);
            _publisher.Publish(codeUnit);
        }
    }
}
