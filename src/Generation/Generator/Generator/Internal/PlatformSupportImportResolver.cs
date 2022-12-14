using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class PlatformSupportImportResolver
{
    private readonly Publisher _publisher;

    public PlatformSupportImportResolver(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var source = Renderer.Internal.PlatformSupportImportResolver.Render(linuxNamespace, macosNamespace, windowsNamespace);

        var codeUnit = new CodeUnit(
            Project: GetProjectName(linuxNamespace, macosNamespace, windowsNamespace),
            Name: "ImportResolver",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }

    private static string GetProjectName(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var names = new HashSet<string>();
        if (linuxNamespace is not null)
            names.Add(Namespace.GetCanonicalName(linuxNamespace));
        if (macosNamespace is not null)
            names.Add(Namespace.GetCanonicalName(macosNamespace));
        if (windowsNamespace is not null)
            names.Add(Namespace.GetCanonicalName(windowsNamespace));

        return names.Count switch
        {
            0 => throw new Exception("Please provie at least one namespace"),
            1 => names.First(),
            _ => throw new Exception("Namespace internal names does not match")
        };
    }
}
