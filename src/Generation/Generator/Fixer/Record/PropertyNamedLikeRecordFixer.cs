using Generator.Model;

namespace Generator.Fixer.Record;

internal class PropertyNamedLikeRecordFixer : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        foreach (var field in record.Fields)
        {
            var name = Field.GetName(field);
            if (name == record.Name)
            {
                Field.SetName(field, $"{name}_");
                Log.Information($"Field {field.Name} is named like its containing record {record.Namespace.Name}.{record.Name}. This is not allowed. The field should be created with a suffix and be rewritten to its original name");
                return;
            }
        }
    }
}
