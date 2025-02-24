using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class String : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.String>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var signatureName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {callName} = {signatureName}.ConvertToString();");
        parameterData.SetCallName(() => callName);
    }
}
