using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class StringArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var arrayType = parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        if (parameterData.Parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            var signatureName = Model.Parameter.GetName(parameterData.Parameter);
            var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

            parameterData.SetSignatureName(signatureName);
            parameterData.SetExpression($"var {callName} = GLib.Internal.StringHelper.ToStringArrayUtf8({signatureName});");
            parameterData.SetCallName(callName);
        }
        else
        {
            var variableName = Model.Parameter.GetName(parameterData.Parameter);

            parameterData.SetSignatureName(variableName);
            parameterData.SetCallName(variableName);
        }
    }
}
