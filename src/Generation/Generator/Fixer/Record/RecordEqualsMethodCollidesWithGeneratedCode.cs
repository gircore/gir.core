using Generator.Model;

namespace Generator.Fixer.Record;

internal class RecordEqualsMethodCollidesWithGeneratedCode : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        if (Model.Record.IsTyped(record) || Model.Record.IsOpaqueTyped(record) || Model.Record.IsOpaqueUntyped(record))
        {
            foreach (var method in record.Methods)
            {
                if (Method.GetPublicName(method) == "Equals")
                {
                    Method.SetPublicName(method, "NativeEquals");
                    Log.Warning(
                        $"Method {record.Name}.{method.Name} is named 'equals' which collides with the generated code. The method get's renamed to 'NativeEquals'");
                }
            }
        }
    }
}
