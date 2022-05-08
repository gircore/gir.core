using Generator.Model;

namespace Generator.Fixer;

internal static class RecordFixer
{
    public static void Fixup(GirModel.Record record)
    {
        FixInternalMethodsNamedLikeRecord(record);
    }

    private static void FixInternalMethodsNamedLikeRecord(GirModel.Record record)
    {
        foreach (var method in record.Methods)
        {
            if (Method.GetInternalName(method) == record.Name)
            {
                Method.Disable(method);
                Log.Warning($"Method {method.Name} is named like its containing class. This is not allowed. The method should be created with a suffix and be rewritten to it's original name");
            }
        }
    }
}
