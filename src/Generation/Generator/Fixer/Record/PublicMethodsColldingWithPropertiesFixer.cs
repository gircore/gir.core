using Generator.Model;

namespace Generator.Fixer.Record;

internal class PublicMethodsColldingWithFieldFixer : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        foreach (var field in record.Fields)
        {
            foreach (var method in record.Methods)
            {
                if (Field.GetName(field) == Method.GetPublicName(method))
                {
                    if (method.ReturnType.AnyType.Is<GirModel.Void>())
                    {
                        Log.Warning($"{record.Namespace.Name}.{record.Name}: Method {Method.GetPublicName(method)} is named like a field but returns no value. It makes no sense to prefix the method with 'Get'. Thus it will be ignored.");
                        Method.Disable(method);
                    }
                    else
                    {
                        var newName = $"Get{Method.GetPublicName(method)}";
                        Log.Warning($"{record.Namespace.Name}.{record.Name}: Method {Method.GetPublicName(method)} collides with field {Field.GetName(field)} and is renamed to {newName}.");

                        Method.SetPublicName(method, newName);
                    }
                }
            }
        }
    }
}
