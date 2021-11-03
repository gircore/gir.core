using Generator3.Publication;
using Generator3.Generation.Framework;

namespace Generator3
{
    public static class Framework
    {
        public static void Generate(string project, string @namespace)
        {
            var nativeExtensionsGenerator = new NativeExtensionsGenerator (
                template: new NativeExtensionsTemplate(),
                publisher: new NativeClassFilePublisher()
            );
            
            nativeExtensionsGenerator.Generate(project, @namespace);
        }
    }
}
