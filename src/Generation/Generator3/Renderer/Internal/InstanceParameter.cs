namespace Generator3.Renderer.Internal
{
    public static class InstanceParameter
    {
        public static string Render(this Model.Internal.InstanceParameter parameter)
            => $@"{parameter.NullableTypeName} {parameter.Name}";
    }
}
