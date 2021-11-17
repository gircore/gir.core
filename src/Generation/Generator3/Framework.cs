using Generator3.Publication;
using Generator3.Generation.Framework;

namespace Generator3
{
    public static class Framework
    {
        public static void Generate(string project, string sharedLibrary, string @namespace)
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

            internalExtensionsGenerator.Generate(project, @namespace);
            internalDllImportGenerator.Generate(project, sharedLibrary, @namespace);
            moduleDllImportGenerator.Generate(project, @namespace);
        }
    }
}
