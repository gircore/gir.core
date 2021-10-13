using System.Collections.Generic;
using Generator3.Generation.Functions;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly NativeGenerator _nativeFunctionsGenerator = new (
            renderer: new Renderer.NativeFunctionsUnit(),
            publisher: new NativeClassFilePublisher()
        );

        public void GenerateFunctions(string project, IEnumerable<GirModel.Method> functions)
        {
            _nativeFunctionsGenerator.Generate(project, functions);
        }
    }
}
