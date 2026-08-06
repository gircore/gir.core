using System.Linq;

namespace Generator.Fixer.Record;

internal class PublicMethodsWithCallbackReturnWhichIsFundamental : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        foreach (var method in record.Methods)
        {
            try
            {
                var parameter = method.Parameters.FirstOrDefault(x =>
                    x.AnyTypeReferenceOrVarArgs.TryPickT0(out var anyTypeReference, out _)
                    && anyTypeReference.TryPickT0(out var typeReference, out _)
                    && typeReference.Type is GirModel.Callback callback
                    && callback.ReturnType.AnyTypeReference.TryPickT0(out var typeReference2, out _)
                    && typeReference2.Type is GirModel.Class { Fundamental: true }
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
