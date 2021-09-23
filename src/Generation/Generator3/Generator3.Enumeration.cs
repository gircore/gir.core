namespace Generator3
{
    public partial class Generator3
    {
        private Generation.Enumeration.Generator? _enumerationGenerator;

        private Generation.Enumeration.Generator EnumerationGenerator
            => _enumerationGenerator ??= new Generation.Enumeration.Generator(new Rendering.Scriban.Renderers.Enumeration());

        private Publication.EnumerationPublisher EnumerationPublisher => _publisher;

        public void GenerateEnumeration(string project, GirModel.Enumeration enumeration)
        {
            var source = EnumerationGenerator.Generate(enumeration);
            var codeUnit = new Publication.CodeUnit(project, enumeration.Name, source);
            EnumerationPublisher.Publish(codeUnit);
        }
    }
}
