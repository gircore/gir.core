using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Callback
{
    public class NativeGenerator
    {
        private readonly Renderer<Model.NativeCallbackUnit> _renderer;
        private readonly Publisher _publisher;

        public NativeGenerator(Renderer<Model.NativeCallbackUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Callback callback)
        {
            var model = new Model.NativeCallbackUnit(callback);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, callback.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
