namespace Generator3.Renderer.Native
{
    public static class ReturnType
    {
        public static string Render(this Model.Native.ReturnType returnType)
            => $@"{ returnType.NullableTypeName }";
    }
}
