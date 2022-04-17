using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class PublicModuleTypeRegistrationGenerator
    {
        private readonly Template<PublicModuleTypeRegistrationModel> _template;
        private readonly Publisher _publisher;

        public PublicModuleTypeRegistrationGenerator(Template<PublicModuleTypeRegistrationModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Namespace ns)
        {
            if (ns.Name == "GLib")
                return;//We can not register any type of GLib as GLib is not using the GObject type system

            try
            {
                var model = new PublicModuleTypeRegistrationModel(ns);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(ns.GetCanonicalName(), "Module.TypeRegistration", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate Module.TypeRegistration");
                throw;
            }
        }
    }
}
