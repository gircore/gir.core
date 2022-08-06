using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Callbacks
{
    public static void Generate(this IEnumerable<GirModel.Callback> callbacks, string path)
    {
        var publisher = new Publisher(path);

        var generators = new List<Generator<GirModel.Callback>>()
        {
            new Generator.Internal.CallbackDelegate(publisher),
            new Generator.Public.CallbackHandler(publisher),
            new Generator.Public.CallbackAsyncHandler(publisher),
            new Generator.Public.CallbackDelegate(publisher),
            new Generator.Public.CallbackNotifiedHandler(publisher)
        };

        foreach (var callback in callbacks)
            foreach (var generator in generators)
                generator.Generate(callback);
    }
}
