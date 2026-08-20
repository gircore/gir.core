using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Interface : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Interface>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeReferenceOrVarArgs}: Unpointed interface parameter not yet supported");

        var iface = (GirModel.Interface) parameterData.Parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type;

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {callName} = ({Model.Type.GetPublicNameFullyQuallified(iface)}) GObject.Internal.InstanceWrapper.WrapHandle<{Model.Interface.GetFullyQualifiedImplementationName(iface)}>({signatureName}, {Model.Transfer.IsOwnedRef(parameterData.Parameter.Transfer).ToString().ToLower()});");
        parameterData.SetCallName(() => callName);
    }
}
