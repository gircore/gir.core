using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Record
{
    public class NativeStructGenerator
    {
        private readonly Renderer<Model.NativeRecordStructUnit> _renderer;
        private readonly Publisher _publisher;

        public NativeStructGenerator(Renderer<Model.NativeRecordStructUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Record record)
        {
            var model = new Model.NativeRecordStructUnit(record);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, $"{record.Name}.Struct", source);
            _publisher.Publish(codeUnit);
        }
    }
}
