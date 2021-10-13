using Generator3.Generation.Enumeration;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly Generator _enumGenerator = new (
            renderer: new Renderer.EnumerationUnit(),
            publisher: new EnumFilePublisher()
        );
        
        public void GenerateEnumeration(string project, GirModel.Enumeration enumeration)
        {
            _enumGenerator.Generate(project, enumeration);
        }
    }
}
