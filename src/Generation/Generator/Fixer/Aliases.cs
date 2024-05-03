using System.Collections.Generic;
using Generator.Fixer.Record;

namespace Generator.Fixer;

public static class Aliases
{
    private static readonly List<Fixer<GirModel.Alias>> Fixers = new()
    {
       new DisableGObjectGTypeAlias()
    };

    public static void Fixup(IEnumerable<GirModel.Alias> aliases)
    {
        foreach (var alias in aliases)
            foreach (var fixer in Fixers)
                fixer.Fixup(alias);
    }
}
