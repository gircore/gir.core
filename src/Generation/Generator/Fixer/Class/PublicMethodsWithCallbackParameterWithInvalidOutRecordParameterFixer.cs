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
            var parameter = method.Parameters.FirstOrDefault(x => x.AnyTypeReferenceOrVarArgs.TryPickT0(out var anyTypeReference, out _)
                                                                  && anyTypeReference.TryPickT0(out var typeReference, out _)
                                                                  && typeReference.Type is GirModel.Callback c
                                                                  && c.Parameters.Any(y => y.Direction == GirModel.Direction.Out && y.AnyTypeReferenceOrVarArgs.TryPickT0(out var anyTypeReference2, out _)
                                                                  && anyTypeReference2.TryPickT0(out var typeReference2, out _)
                                                                  && typeReference2.Type is GirModel.Record));

            if (parameter is null)
                continue;

            Log.Warning($"Disabling method {method.CIdentifier} as it has a callback parameter that contains an out record parameter. This is currently not supported.");
            Model.Method.Disable(method);
        }
    }
}
