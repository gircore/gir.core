using System.Collections.Generic;
using Generator.Fixer;
using Generator.Generator;

namespace Generator;

public static class Bitfields
{
    public static void Generate(this IEnumerable<GirModel.Bitfield> bitfields, string path)
    {
        var publisher = new Publisher(path);
        var generator = new Generator.Public.Bitfield(publisher);

        foreach (var bitfield in bitfields)
        {
            BitfieldFixer.Fixup(bitfield);
            generator.Generate(bitfield);
        }
    }
}
