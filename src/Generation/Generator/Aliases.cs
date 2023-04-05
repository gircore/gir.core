using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Aliases
{
    public static void Generate(IEnumerable<GirModel.Alias> aliases, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Alias>>
        {
            new Generator.Public.AliasPrimitiveValueType(publisher)
        };

        foreach (var alias in aliases)
            foreach (var generator in generators)
                generator.Generate(alias);
    }
}
