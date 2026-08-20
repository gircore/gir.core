using System;
using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Class : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Class>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException($"{returnType.AnyTypeReference}: class return type which is no pointer can not be converted to native");

        return returnType.Nullable
            ? fromVariableName + "?.Handle.DangerousGetHandle() ?? IntPtr.Zero"
            : fromVariableName + ".Handle.DangerousGetHandle()";
    }
}
