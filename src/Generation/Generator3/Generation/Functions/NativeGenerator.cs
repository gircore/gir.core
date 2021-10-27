using System.Collections.Generic;

namespace Generator3.Generation.Functions
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

        public void Generate(string project, IEnumerable<GirModel.Method> functions)
        {
            var model = new NativeModel(functions);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(project, "Functions", source);
            _publisher.Publish(codeUnit);
        }
    }
}
