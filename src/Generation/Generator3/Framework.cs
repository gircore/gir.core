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

            var project = ns.GetCanonicalName();

            
            if (ns.SharedLibrary is null)
                Log.Warning($"Shared library name is not set for project {ns.GetCanonicalName()}. {ns.GetCanonicalName()} method calls will fail as the shared library won't be found.");
            else
                internalDllImportGenerator.Generate(project, ns.SharedLibrary, ns.Name);
            
            internalExtensionsGenerator.Generate(project, ns.Name);
            moduleDllImportGenerator.Generate(project, ns.Name);
            moduleTypeRegistration.Generate(project, ns);
        }
    }
}
