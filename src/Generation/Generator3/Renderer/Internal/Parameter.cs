namespace Generator3.Renderer.Internal
{
    public static class Parameter
    {
        public static string Render(this Model.Internal.Parameter parameter)
            => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
    }
}
