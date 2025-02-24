using Generator.Model;

namespace Generator.Fixer.Record;

internal class InternalMethodsNamedLikeRecordFixer : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
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
