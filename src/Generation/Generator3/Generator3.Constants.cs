using System.Collections.Generic;

namespace Generator3
{
    public partial class Generator3
    {
        private Generation.Unit.Constants.Generator? _constantsGenerator;

        private Generation.Unit.Constants.Generator ConstantsGenerator
            => _constantsGenerator ??= new Generation.Unit.Constants.Generator(new Rendering.Templates.Constants());

        private Publication.ConstantsPublisher ConstantsPublisher => _publisher;

        public void GenerateConstants(string project, IEnumerable<GirModel.Constant> constants)
        {
            var source = ConstantsGenerator.Generate(constants);
            var codeUnit = new Publication.CodeUnit(project, "Constants", source);
            ConstantsPublisher.Publish(codeUnit);
        }
    }
}
