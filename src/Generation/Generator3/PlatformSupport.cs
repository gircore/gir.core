using Generator3.Generation.Framework;
using Generator3.Publication;

namespace Generator3;

public static class PlatformSupport
{
    public static void GeneratePlatform(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var internalImportResolverGenerator = new InternalImportResolverGenerator(
            template: new InternalImportResolverTemplate(),
            publisher: new InternalPlatformSupportFilePublisher()
        );

        internalImportResolverGenerator.Generate(linuxNamespace, macosNamespace, windowsNamespace);
    }
}
