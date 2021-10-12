namespace Generator3.Rendering.Templates
{
    public static class ReturnType
    {
        public static string Get(Generation.Model.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
