namespace Generator3.Renderer.Public
{
    public static class ConvertReturnType
    {
        public static string RenderTo(this Model.Internal.ReturnType internalReturnType, Model.Public.ReturnType publicReturnType)
        {
            return (internalReturnType, publicReturnType) switch
            {
                _ => $"//TODO {internalReturnType.NullableTypeName} -> {publicReturnType.NullableTypeName}"
            };
        }
    }
}
