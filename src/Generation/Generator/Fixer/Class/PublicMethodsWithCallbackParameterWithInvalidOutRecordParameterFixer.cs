using System.Linq;

namespace Generator.Fixer.Class;

internal class PublicMethodsWithCallbackParameterWithInvalidOutRecordParameterFixer : Fixer<GirModel.Class>
{
    //TODO: This fixer exists because the record support is not good enough currently to support this scenario.
    //As soon as records are better supported this can be fixed in the generator project.

    public void Fixup(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            var parameter = method.Parameters.FirstOrDefault(x => x.AnyTypeOrVarArgs.TryPickT0(out var anyType, out _)
                                                                  && anyType.TryPickT0(out var type, out _)
                                                                  && type is GirModel.Callback c
                                                                  && c.Parameters.Any(y => y.Direction == GirModel.Direction.Out && y.AnyTypeOrVarArgs.TryPickT0(out var anyType2, out _)
                                                                  && anyType2.TryPickT0(out var type2, out _)
                                                                  && type2 is GirModel.Record));

            if (parameter is null)
                continue;

            Log.Warning($"Disabling method {method.CIdentifier} as it has a callback parameter that contains an out record parameter. This is currently not supported.");
            Model.Method.Disable(method);
        }
    }
}
