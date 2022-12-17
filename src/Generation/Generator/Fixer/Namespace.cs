using System.Collections.Generic;
using Generator.Fixer.Ns;

namespace Generator.Fixer;

public static class Namespace
{
    private static readonly List<Fixer<GirModel.Namespace>> Fixers = new()
    {
        new DisableRecordsConflictingWithGObjectsFixer()
    };

    public static void Fixup(GirModel.Namespace ns)
    {
        foreach (var fixer in Fixers)
            fixer.Fixup(ns);
    }
}
