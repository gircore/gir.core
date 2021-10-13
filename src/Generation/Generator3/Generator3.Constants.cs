using System.Collections.Generic;
using Generator3.Generation.Constants;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly Generator _constantsGenerator = new (
            renderer: new Renderer.ConstantsUnit(),
            publisher: new ClassFilePublisher()
        );
        
        public void GenerateConstants(string project, IEnumerable<GirModel.Constant> constants)
        {
            _constantsGenerator.Generate(project, constants);
        }
    }
}
