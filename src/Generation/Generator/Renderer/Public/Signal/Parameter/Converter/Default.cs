using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.Signals;

public class Default : SignalArgsParameterConverter
{
    public bool Supports(AnyType type)
    {
        return true;
    }

    public void Initialize(SignalArgsParameterData parameter, int index, IEnumerable<SignalArgsParameterData> parameters)
    {
        var p = ParameterRenderer.Render(parameter.Parameter);
        parameter.SetExpression(() => $"public {p.NullableTypeName} {parameter.Parameter.Name.ToPascalCase()} => Extract<{p.NullableTypeName}>(Args[{index}]);");
    }
}
