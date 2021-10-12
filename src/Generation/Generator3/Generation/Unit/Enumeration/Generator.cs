namespace Generator3.Generation.Unit.Enumeration
{
    public class Generator
    {
        private readonly Renderer _renderer;

        public Generator(Renderer renderer)
        {
            _renderer = renderer;
        }

        public string Generate(GirModel.Enumeration enumeration)
        {
            var data = new Model(
                name: enumeration.Name,
                namespaceName: enumeration.NamespaceName
            );

            foreach (var member in enumeration.Members)
                data.Add(new Generation.Model.Member(member));

            return _renderer.Render(data);
        }
    }
}
