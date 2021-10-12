using System.Collections.Generic;

namespace Generator3
{
    public partial class Generator3
    {
        private Generation.Unit.Native.Functions.Generator? _functionsGenerator;

        private Generation.Unit.Native.Functions.Generator NativeFunctionsGenerator
            => _functionsGenerator ??= new Generation.Unit.Native.Functions.Generator(new Rendering.Templates.NativeFunctions());

        private Publication.NativeFunctionsPublisher NativeFunctionsPublisher => _publisher;

        public void GenerateFunctions(string project, IEnumerable<GirModel.Method> functions)
        {
            var source = NativeFunctionsGenerator.Generate(functions);
            var codeUnit = new Publication.CodeUnit(project, "Functions", source);
            NativeFunctionsPublisher.Publish(codeUnit);
        }
    }
}
