using GirModel;
using Method = Generator.Model.Method;

namespace Generator.Fixer.Record;

public class MethodWithInOutInstanceParameterFixer : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        foreach (var method in record.Methods)
        {
            if (method.InstanceParameter.Direction == Direction.InOut)
            {
                Method.Disable(method);
                Log.Warning($"Method {method.Name} has an instance parameter with direction inout which is not supported. Method is disabled.");
            }
        }
    }
}
