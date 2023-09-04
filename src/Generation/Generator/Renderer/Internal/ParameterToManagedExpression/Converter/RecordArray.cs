using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class RecordArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>(out var record) && Model.Record.IsStandard(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var arrayType = parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        var record = (GirModel.Record) arrayType.AnyType.AsT0;
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);
        var signatureName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(signatureName);
        parameterData.SetCallName(callName);

        if (arrayType.IsPointer)
        {
            parameterData.SetExpression($"var {callName} = {signatureName}.Select(x => new {Model.Record.GetFullyQualifiedPublicClassName(record)}(x)).ToArray();");
        }
        else
        {
            parameterData.SetExpression($"var {callName} = ({Model.Record.GetFullyQualifiedPublicClassName(record)}[]){signatureName}.Select(x => new {Model.Record.GetFullyQualifiedPublicClassName(record)}({Model.Record.GetFullyQualifiedInternalManagedHandleCreateMethod(record)}(x))).ToArray();");
        }
    }
}
