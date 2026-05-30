using System.Linq;

namespace Generator.Fixer.Class;

internal class PublicMethodsWithCallbackReturnWhichIsFundamental : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            try
            {
                var parameter = method.Parameters.FirstOrDefault(x =>
                    x.AnyTypeOrVarArgs.TryPickT0(out var anyType, out _)
                    && anyType.TryPickT0(out var type, out _)
                    && type is GirModel.Callback callback
                    && callback.ReturnType.AnyType.TryPickT0(out var returnType, out _)
                    && returnType is GirModel.Class { Fundamental: true }
                );

                if (parameter is null)
                    continue;

                Log.Warning($"Disabling method {method.CIdentifier} as it uses a disabled callback as return type.");
                Model.Method.Disable(method);
            }
            catch (System.Exception ex)
            {
                Log.Warning($"Disabling method {method.CIdentifier} because an exception occurred: {ex}");
                Model.Method.Disable(method);
            }

        }
    }
}
