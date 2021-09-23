using System.Collections.Generic;

namespace Generator3
{
    public partial class Generator3
    {
        private Generation.NativeFunctions.Generator? _functionsGenerator;

        private Generation.NativeFunctions.Generator NativeFunctionsGenerator
            => _functionsGenerator ??= new Generation.NativeFunctions.Generator(new Rendering.Scriban.Renderers.NativeFunctions());

        private Publication.NativeFunctionsPublisher NativeFunctionsPublisher => _publisher;

        public void GenerateFunctions(string project, IEnumerable<GirModel.Method> functions)
        {
            var source = NativeFunctionsGenerator.Generate(functions);
            var codeUnit = new Publication.CodeUnit(project, "Functions", source);
            NativeFunctionsPublisher.Publish(codeUnit);
        }
    }
}
