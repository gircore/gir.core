using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class InternalImportResolverGenerator
    {
        private readonly Template<InternalImportResolverModel> _template;
        private readonly Publisher _publisher;

        public InternalImportResolverGenerator(Template<InternalImportResolverModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
        {
            try
            {
                var model = new InternalImportResolverModel(linuxNamespace, macosNamespace, windowsNamespace);
                var source = _template.Render(model);
                var projectName = GetProjectName(linuxNamespace, macosNamespace, windowsNamespace);
                var codeUnit = new CodeUnit(projectName, "ImportResolver", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning("Could not generate DllImport");
                throw;
            }
        }

        private string GetProjectName(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
        {
            var names = new HashSet<string>();
            if (linuxNamespace is not null)
                names.Add(linuxNamespace.GetCanonicalName());
            if (macosNamespace is not null)
                names.Add(macosNamespace.GetCanonicalName());
            if (windowsNamespace is not null)
                names.Add(windowsNamespace.GetCanonicalName());

            return names.Count switch
            {
                0 => throw new Exception("Please provie at least one namespace"),
                1 => names.First(),
                _ => throw new Exception("Namespace internal names does not match")
            };
        }
    }
}
