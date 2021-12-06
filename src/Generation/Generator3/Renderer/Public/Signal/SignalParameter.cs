using Generator3.Extensions;

namespace Generator3.Renderer.Public
{
    public static class SignalParameter
    {
        public static string RenderAsSignalParammeter(this Model.Public.Parameter parameter, int index)
            => $@"public {parameter.NullableTypeName} {parameter.Name.ToPascalCase()} => Args[{index}].Extract<{parameter.NullableTypeName}>();";
    }
}
