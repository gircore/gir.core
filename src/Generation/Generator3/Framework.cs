using System;
using Generator3.Converter;
using Generator3.Publication;
using Generator3.Generation.Framework;

namespace Generator3
{
    public static class Framework
    {
        public static void GenerateFramework(this GirModel.Namespace ns)
        {
            var internalExtensionsGenerator = new InternalExtensionsGenerator (
                template: new InternalExtensionsTemplate(),
                publisher: new InternalFrameworkFilePublisher()
            );

            var internalDllImportGenerator = new InternalDllImportGenerator(
                template: new InternalDllImportTemplate(),
                publisher: new InternalFrameworkFilePublisher()
            );
            
            var moduleDllImportGenerator = new ModuleDllImportGenerator(
                template: new ModuleDllImportTemplate(),
                publisher: new PublicFrameworkFilePublisher()
            );

            var moduleTypeRegistration = new PublicModuleTypeRegistrationGenerator(
                template: new PublicModuleTypeRegistrationTemplate(),
                publisher: new PublicFrameworkFilePublisher()
            );
            
            if (ns.SharedLibrary is null)
                throw new Exception($"Shared library is not set for project {ns.GetCanonicalName()}");
            
            var project = ns.GetCanonicalName();

            internalExtensionsGenerator.Generate(project, ns.Name);
            internalDllImportGenerator.Generate(project, ns.SharedLibrary, ns.Name);
            moduleDllImportGenerator.Generate(project, ns.Name);
            moduleTypeRegistration.Generate(project, ns);
        }
    }
}
