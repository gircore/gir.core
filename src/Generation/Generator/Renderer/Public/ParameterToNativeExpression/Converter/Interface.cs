using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Interface : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Interface parameter with direction != in not yet supported");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var callParameter = parameter.Parameter.Nullable
            ? $"((GObject.Object?){parameterName})?.Handle.DangerousGetHandle() ?? IntPtr.Zero"
            : $"((GObject.Object){parameterName}).Handle.DangerousGetHandle()";

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => callParameter);

        // If there is an ownership transfer, the called function will not add
        // a ref but will hold onto the object and later remove a ref.
        // However, the original owned ref still exists (e.g. the managed object's handle)
        // so we need to add an extra ref to account for the remaining owner.
        if (Transfer.IsOwnedRef(parameter.Parameter.Transfer))
        {
            var addRefExpression = parameter.Parameter.Nullable
                ? $"if(((GObject.Object?){parameterName})?.Handle.DangerousGetHandle() is System.IntPtr ptr) GObject.Internal.Object.Ref(ptr);"
                : $"GObject.Internal.Object.Ref(((GObject.Object){parameterName}).Handle.DangerousGetHandle());";
            parameter.SetExpression(() => addRefExpression);
        }
    }
}
