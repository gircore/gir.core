namespace Generator3.Renderer.Public
{
    public static class InstanceParameter
    {
        public static string Render(this Model.Public.InstanceParameter parameter)
            => $@"{parameter.NullableTypeName} {parameter.Name}";
    }
}
