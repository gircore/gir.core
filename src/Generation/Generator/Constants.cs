using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Constants
{
    public static void Generate(this IEnumerable<GirModel.Constant> constants, string path)
    {
        var publisher = new Publisher(path);
        var generator = new Generator.Public.Constants(publisher);
        generator.Generate(constants);
    }
}
