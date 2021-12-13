using System;

namespace Generator3.Renderer.Public
{
    public static class MethodParameterExpression
    {
        public static string RenderPublicParameterExpression(this Model.Public.Parameter parameter)
        {
            if (parameter.Model.IsPointer)
                throw new Exception($"Parameter {parameter.Name}: Pointer parameters not yet supported");

            if (parameter.Model.AnyType.Is<GirModel.PrimitiveValueType>())
                return $"var {parameter.GetMethodParameterVariable()} = {parameter.Name};";

            throw new Exception($"Can not convert parameter {parameter.Name} of type {parameter.NullableTypeName}");
        }
    }
}
