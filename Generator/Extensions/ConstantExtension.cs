using GirLoader.Output.Model;

namespace Generator
{
    internal static class ConstantExtension
    {
        public static string WriteManaged(this Constant constant)
        {
            var value = GetValue(constant);

            var referencedTypeName = constant.TypeReference.GetResolvedType().Name;
            return $"public static {referencedTypeName} {constant.Name} = {value};\r\n";
        }

        private static string GetValue(Constant constant)
        {
            var referencedTypeName = constant.TypeReference.GetResolvedType().Name;
            var name = referencedTypeName.Value;

            if (name.EndsWith("Flags"))
                return $"({name}) {constant.Value}";

            if (name == "string")
                return "\"" + constant.Value + "\"";

            return constant.Value;
        }
    }
}
