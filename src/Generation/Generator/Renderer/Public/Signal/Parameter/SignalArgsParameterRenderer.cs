using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Renderer.Public.Signals;

public static class SignalArgsParameterRenderer
{
    private static readonly List<SignalArgsParameterConverter> Converter =
    [
        new ClassArray(),
        new InterfaceArray(),
        new Default() //Must be last as a fallback
    ];

    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        var sb = new StringBuilder();
        var index = 1; //Argument 0 is reserved

        var data = parameters
            .Select(x => new SignalArgsParameterData(x))
            .ToList();

        foreach (var parameter in data)
        {
            var converterFound = false;

            foreach (var converter in Converter)
            {
                if (parameter.Parameter.AnyTypeOrVarArgs.IsT1)
                    throw new Exception("Variadic parameters are not yet supported");

                if (converter.Supports(parameter.Parameter.AnyTypeOrVarArgs.AsT0))
                {
                    converter.Initialize(parameter, index, data);
                    converterFound = true;
                    break;
                }
            }

            if (!converterFound)
                throw new NotImplementedException($"Missing converter to convert from signal parameter {parameter.Parameter} ({parameter.Parameter.AnyTypeOrVarArgs}) to signal args");

            index++;
        }

        foreach (var parameter in data)
        {
            if (parameter.IsArrayLengthParameter)
                continue;

            sb.AppendLine(parameter.GetExpression());
        }

        return sb.ToString();
    }
}
