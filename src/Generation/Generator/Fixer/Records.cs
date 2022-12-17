using System.Collections.Generic;
using Generator.Fixer.Record;

namespace Generator.Fixer;

public static class Records
{
    private static readonly List<Fixer<GirModel.Record>> Fixers = new()
    {
        new InternalMethodsNamedLikeRecordFixer()
    };

    public static void Fixup(IEnumerable<GirModel.Record> records)
    {
        foreach (var record in records)
            foreach (var fixer in Fixers)
                fixer.Fixup(record);
    }
}
