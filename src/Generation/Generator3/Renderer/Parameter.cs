namespace Generator3.Renderer
{
    public static class Parameter
    {
        public static string Render(this Model.Parameter parameter)
            => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
    }
}
