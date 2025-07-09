using System;
using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Interfaces
{
    public static void Generate(IEnumerable<GirModel.Interface> interfaces, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Interface>>
        {
            new Generator.Internal.InterfaceMethods(publisher),
            new Generator.Public.Interface(publisher),

            new Generator.Public.InterfaceImplementationFramework(publisher),
            new Generator.Public.InterfaceImplementationMethods(publisher),
            new Generator.Public.InterfaceImplementationFunctions(publisher),
            new Generator.Public.InterfaceImplementationProperties(publisher),
            new Generator.Public.InterfaceImplementationSignals(publisher)
        };

        foreach (var iface in interfaces)
        {
            foreach (var generator in generators)
            {
                try
                {
                    generator.Generate(iface);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    Log.Error($"Could not render interface with generator {generator.GetType().Name}: {iface.Namespace}.{iface.Name} - {ex.Message}");
                }
            }
        }
    }
}
