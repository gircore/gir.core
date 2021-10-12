namespace Generator3.Generation.Unit.Native.RecordFunctions
{
    public class Generator
    {
        private readonly Renderer<Model> _renderer;

        public Generator(Renderer<Model> renderer)
        {
            _renderer = renderer;
        }

        public string Generate(GirModel.Record @record)
        {
            var data = new Model(
                name: record.Name,
                namespaceName: record.NamespaceName + ".Native"
            );

            return _renderer.Render(data);
        }
    }
}
