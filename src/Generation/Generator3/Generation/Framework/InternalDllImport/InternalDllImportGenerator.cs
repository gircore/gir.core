namespace Generator3.Generation.Framework
{
    public class InternalDllImportGenerator
    {
        private readonly Template<InternalDllImportModel> _template;
        private readonly Publisher _publisher;

        public InternalDllImportGenerator(Template<InternalDllImportModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(string project, GirModel.Namespace ns)
        {
            try
            {
                var model = new InternalDllImportModel(ns);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(project, "DllImport", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate DllImport");
                throw;
            }
        }
    }
}
