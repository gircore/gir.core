using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Interfaces
{
    public static void Generate(IEnumerable<GirModel.Interface> interfaces, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Interface>>()
        {
            new Generator.Internal.InterfaceMethods(publisher),
            new Generator.Public.InterfaceFramework(publisher),
            new Generator.Public.InterfaceMethods(publisher),
            new Generator.Public.InterfaceProperties(publisher),

            new Generator.Public.InterfaceImplementationFramework(publisher),
            new Generator.Public.InterfaceImplementationMethods(publisher),
            new Generator.Public.InterfaceImplementationProperties(publisher)
        };

        foreach (var iface in interfaces)
            foreach (var generator in generators)
                generator.Generate(iface);
    }
}
