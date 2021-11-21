namespace Generator3.Renderer.Public
{
    public static class ReturnType
    {
        public static string Render(this Model.Public.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
