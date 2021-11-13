namespace Generator3.Renderer.Native
{
    public static class Parameter
    {
        public static string Render(this Model.Native.Parameter parameter)
            => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
    }
}
