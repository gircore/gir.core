using System.Collections.Generic;
using Generator.Fixer.Callback;

namespace Generator.Fixer;

public static class Callbacks
{
    private static readonly List<Fixer<GirModel.Callback>> Fixers = [
        new DisableFundamentalReturnTypes()
    ];

    public static void Fixup(IEnumerable<GirModel.Callback> callbacks)
    {
        foreach (var callback in callbacks)
            foreach (var fixer in Fixers)
                fixer.Fixup(callback);
    }
}
