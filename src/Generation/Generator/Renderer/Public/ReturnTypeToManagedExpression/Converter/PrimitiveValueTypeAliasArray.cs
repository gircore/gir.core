using System;
using System.Collections.Generic;
using System.Linq;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PrimitiveValueTypeAliasArray : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.IsArrayAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parameters)
    {
        if (data.ReturnType.AnyType.AsT1.IsZeroTerminated)
            throw new NotImplementedException("Zero-terminated return type is not supported");

        var lengthParameterIndex = data.ReturnType.AnyType.AsT1.Length ?? throw new Exception("Length Parameter not filled");
        var lengthParameter = parameters.ElementAt(lengthParameterIndex);
        lengthParameter.IsArrayLengthParameter = true;

        data.SetPostReturnStatement(returnVariable =>
        {
            var typeName = Model.Type.GetPublicNameFullyQuallified(data.ReturnType.AnyType.AsT1.AnyType.AsT0);

            return $$"""
                    var resultArray = GLib.Internal.StructArray.Copy<{{typeName}}>({{returnVariable}}, (uint) {{lengthParameter.GetSignatureName()}});
                    {{RenderFreeStatement(data, returnVariable)}}
                   """;
        });

        data.SetExpression(returnVariable => "resultArray");
    }

    private static string RenderFreeStatement(ReturnTypeToManagedData data, string returnVariable)
    {
        return data.ReturnType.Transfer == Transfer.Full
            ? $"GLib.Functions.Free({returnVariable});"
            : string.Empty;
    }
}
