using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Record
{
    public class NativeFunctionsGenerator
    {
        private readonly Renderer<Model.NativeRecordFunctionsUnit> _renderer;
        private readonly Publisher _publisher;

        public NativeFunctionsGenerator(Renderer<Model.NativeRecordFunctionsUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Record record)
        {
            var model = new Model.NativeRecordFunctionsUnit(record);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}.Functions", source);
            _publisher.Publish(codeUnit);
        }
    }
}
