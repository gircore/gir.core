using Generator.Generator;

namespace Generator;

public static class PlatformSupport
{
    public static void GeneratePlatform(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace, string path)
    {
        var publisher = new Publisher(path);
        var generator = new Generator.Internal.PlatformSupportImportResolver(publisher);
        generator.Generate(linuxNamespace, macosNamespace, windowsNamespace);
    }
}
