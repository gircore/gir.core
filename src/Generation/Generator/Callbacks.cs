using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Callbacks
{
    public static void Generate(IEnumerable<GirModel.Callback> callbacks, string path)
    {
        var publisher = new Publisher(path);

        var generators = new List<Generator<GirModel.Callback>>()
        {
            new Generator.Internal.CallbackDelegate(publisher),
            new Generator.Internal.CallbackCallHandler(publisher),
            new Generator.Internal.CallbackAsyncHandler(publisher),
            new Generator.Internal.CallbackNotifiedHandler(publisher),
            new Generator.Internal.CallbackForeverHandler(publisher),
            new Generator.Public.CallbackDelegate(publisher),
        };

        foreach (var callback in callbacks)
            foreach (var generator in generators)
                generator.Generate(callback);
    }
}
