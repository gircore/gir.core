using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Framework
{
    public static void Generate(GirModel.Namespace ns, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Namespace>>()
        {
            new Generator.Internal.FrameworkExtensions(publisher),
            new Generator.Internal.FrameworkTypeRegistration(publisher),
            new Generator.Public.FrameworkVersionAttribute(publisher)
        };

        foreach (var generator in generators)
            generator.Generate(ns);
    }
}
