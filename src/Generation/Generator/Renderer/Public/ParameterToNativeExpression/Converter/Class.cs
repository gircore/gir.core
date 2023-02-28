using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Class : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: class parameter with direction != in not yet supported");

        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: class parameter which is no pointer can not be converted to native");

        var parameterName = Parameter.GetName(parameter.Parameter);
        var callParameter = parameter.Parameter.Nullable
            ? parameterName + "?.Handle ?? IntPtr.Zero"
            : parameterName + ".Handle";

        // If there is an ownership transfer, the called function will not add
        // a ref but will hold onto the object and later remove a ref.
        // However, the original owned ref still exists (e.g. the managed object's handle)
        // so we need to add an extra ref to account for the remaining owner.
        if (Transfer.IsOwnedRef(parameter.Parameter.Transfer))
        {
            var addRefExpression = parameter.Parameter.Nullable
                ? $"{parameterName}?.Ref();"
                : $"{parameterName}.Ref();";
            parameter.SetExpression(addRefExpression);
        }

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(callParameter);
    }
}
