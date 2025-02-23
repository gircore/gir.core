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
        var cls = (GirModel.Class) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        if (cls.Fundamental)
            Fundamental(parameter);
        else
            Default(parameter);
    }

    private static void Fundamental(ParameterToNativeData parameter)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: fundamental class parameter with direction != in not yet supported");

        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: fundamental class parameter which is no pointer can not be converted to native");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
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
                ? $"if({parameterName}?.Handle is not null) GObject.Internal.Object.Ref({parameterName}.Handle);"
                : $"GObject.Internal.Object.Ref({parameterName}.Handle);";
            parameter.SetExpression(() => addRefExpression);
        }

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => callParameter);
    }

    private static void Default(ParameterToNativeData parameter)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: class parameter with direction != in not yet supported");

        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: class parameter which is no pointer can not be converted to native");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var callParameter = parameter.Parameter.Nullable
            ? parameterName + "?.Handle.DangerousGetHandle() ?? IntPtr.Zero"
            : parameterName + ".Handle.DangerousGetHandle()";

        // If there is an ownership transfer, the called function will not add
        // a ref but will hold onto the object and later remove a ref.
        // However, the original owned ref still exists (e.g. the managed object's handle)
        // so we need to add an extra ref to account for the remaining owner.
        if (Transfer.IsOwnedRef(parameter.Parameter.Transfer))
        {
            var addRefExpression = parameter.Parameter.Nullable
                ? $"if({parameterName}?.Handle is not null) GObject.Internal.Object.Ref({parameterName}.Handle.DangerousGetHandle());"
                : $"GObject.Internal.Object.Ref({parameterName}.Handle.DangerousGetHandle());";
            parameter.SetExpression(() => addRefExpression);
        }

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => callParameter);
    }
}
