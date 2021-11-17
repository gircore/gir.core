namespace Generator3.Renderer.Internal
{
    public static class ReturnType
    {
        public static string Render(this Model.Internal.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
