using System;
using System.Collections.Generic;
using System.Linq;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PrimitiveValueTypeAliasArray : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.ReferencesArrayAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parameters)
    {
        if (data.ReturnType.AnyTypeReference.AsT1.IsZeroTerminated)
            throw new NotImplementedException("Zero-terminated return type is not supported");

        var lengthParameterIndex = data.ReturnType.AnyTypeReference.AsT1.Length ?? throw new Exception("Length Parameter not filled");
        var lengthParameter = parameters.ElementAt(lengthParameterIndex);
        lengthParameter.IsArrayLengthParameter = true;

        data.SetPostReturnStatement(returnVariable =>
        {
            var typeName = Model.Type.GetPublicNameFullyQuallified(data.ReturnType.AnyTypeReference.AsT1.AnyTypeReference.AsT0.Type);

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
