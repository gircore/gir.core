using Generator3.Generation.Bitfield;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly Generator _bitfieldGenerator = new (
            renderer: new Renderer.BitfieldUnit(),
            publisher: new EnumFilePublisher()
        );
        
        public void GenerateBitfield(string project, GirModel.Bitfield bitfield)
        {
            _bitfieldGenerator.Generate(project, bitfield);
        }
    }
}
