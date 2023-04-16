using System.Linq;
using GirModel;

namespace Generator.Fixer.Class;

internal class PublicMethodsWithInvalidOutParameterFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            var parameter = method.Parameters.FirstOrDefault(x => x is { CallerAllocates: true, Direction: Direction.Out }
                                                                  && x.AnyTypeOrVarArgs.TryPickT0(out var anyType, out _)
                                                                  && anyType.TryPickT1(out var arrayType, out _)
                                                                  && arrayType.Length is not null);

            if (parameter is null)
                continue;

            var lengthParameter = method.Parameters.ElementAt(parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length!.Value);
            if (lengthParameter.Direction == Direction.Out)
            {
                Log.Warning($"Disabling method {method.CIdentifier} as it has a size parameter which is defined as out parameter which is wrong. Consider marking the parameter explicitly as (in) in the doc comments.");
                Model.Method.Disable(method);
            }
        }
    }
}
