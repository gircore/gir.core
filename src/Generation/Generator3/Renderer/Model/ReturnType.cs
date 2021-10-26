namespace Generator3.Renderer
{
    public static class ReturnType
    {
        public static string Render(this Model.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
