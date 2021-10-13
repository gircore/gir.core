namespace Generator3.Renderer
{
    public static class ReturnType
    {
        public static string Get(Model.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
