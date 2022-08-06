using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Functions
{
    public static void Generate(this IEnumerable<GirModel.Function> functions, string path)
    {
        var publisher = new Publisher(path);
        var generator = new Generator.Internal.GlobalFunctions(publisher);
        generator.Generate(functions);
    }
}
