using System.Collections.Generic;
using Generator.Fixer.Bitfield;

namespace Generator.Fixer;

public static class Bitfields
{
    private static readonly List<Fixer<GirModel.Bitfield>> Fixers = new()
    {
        new DisableDuplicateMembersFixer()
    };

    public static void Fixup(IEnumerable<GirModel.Bitfield> bitfields)
    {
        foreach (var bitfield in bitfields)
            foreach (var fixer in Fixers)
                fixer.Fixup(bitfield);
    }
}
