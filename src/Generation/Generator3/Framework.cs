using Generator3.Publication;
using Generator3.Generation.Framework;

namespace Generator3
{
    public static class Framework
    {
        public static void Generate(string project, string sharedLibrary, string @namespace)
        {
            var nativeExtensionsGenerator = new NativeExtensionsGenerator (
                template: new NativeExtensionsTemplate(),
                publisher: new NativeFrameworkFilePublisher()
            );

            var nativeDllImportGenerator = new NativeDllImportGenerator(
                template: new NativeDllImportTemplate(),
                publisher: new NativeFrameworkFilePublisher()
            );
            
            var moduleDllImportGenerator = new ModuleDllImportGenerator(
                template: new ModuleDllImportTemplate(),
                publisher: new FrameworkFilePublisher()
            );

            nativeExtensionsGenerator.Generate(project, @namespace);
            nativeDllImportGenerator.Generate(project, sharedLibrary, @namespace);
            moduleDllImportGenerator.Generate(project, @namespace);
        }
    }
}
