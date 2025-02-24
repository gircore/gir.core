using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Interface : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Unpointed interface parameter not yet supported");

        var iface = (GirModel.Interface) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {callName} = ({Model.Type.GetPublicNameFullyQuallified(iface)}) GObject.Internal.InstanceWrapper.WrapHandle<{Model.Interface.GetFullyQualifiedImplementationName(iface)}>({signatureName}, {Model.Transfer.IsOwnedRef(parameterData.Parameter.Transfer).ToString().ToLower()});");
        parameterData.SetCallName(() => callName);
    }
}
