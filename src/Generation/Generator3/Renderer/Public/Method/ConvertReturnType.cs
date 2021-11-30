using System;

namespace Generator3.Renderer.Public
{
    public static class ConvertReturnType
    {
        public static string ToPublicFromVariable(this Model.Internal.ReturnType from, string variableName)
        {
            return from switch
            {
                 Model.Internal.PrimitiveValueReturnType { IsPointer: false } => variableName,

                _ => throw new NotImplementedException($"Convert from internal return type {from.NullableTypeName} to public")
            };
        }
    }
}
