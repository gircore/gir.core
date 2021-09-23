namespace Generator3.Generation.Enumeration
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
            var data = new Data(
                name: enumeration.Name,
                namespaceName: enumeration.NamespaceName
            );
            
            foreach(var member in enumeration.Members)
                data.Add(new Member(member));

            return _renderer.Render(data);
        }
    }
}
