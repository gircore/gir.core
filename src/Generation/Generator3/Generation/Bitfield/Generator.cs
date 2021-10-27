using Generator3.Publication;
using Generator3.Renderer;

namespace Generator3.Generation.Bitfield
{
    public class Generator
    {
        private readonly Renderer<Model.BitfieldUnit> _renderer;
        private readonly Publisher _publisher;

        public Generator(Renderer<Model.BitfieldUnit> renderer, Publisher publisher)
        {
            _renderer = renderer;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Bitfield bitfield)
        {
            var model = new Model.BitfieldUnit(bitfield);
            var source = _renderer.Render(model);
            var codeUnit = new CodeUnit(project, bitfield.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
