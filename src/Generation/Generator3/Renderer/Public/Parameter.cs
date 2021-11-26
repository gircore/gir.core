namespace Generator3.Renderer.Public
{
    public static class Parameter
    {
        public static string Render(this Model.Public.Parameter parameter)
            => $@"{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
    }
}
