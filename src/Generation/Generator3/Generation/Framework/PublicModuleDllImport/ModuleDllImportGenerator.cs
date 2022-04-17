using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class ModuleDllImportGenerator
    {
        private readonly Template<ModuleDllImportModel> _template;
        private readonly Publisher _publisher;

        public ModuleDllImportGenerator(Template<ModuleDllImportModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Namespace ns)
        {
            try
            {
                var model = new ModuleDllImportModel(ns);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(ns.GetCanonicalName(), "Module.DllImport", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate Module.DllImport");
                throw;
            }
        }
    }
}
