using Repository.Model;

namespace Generator
{
    internal static class ConstantExtension
    {
        public static string WriteManaged(this Constant constant)
        {
            var type = constant.SymbolReference.GetSymbol().ManagedName;

            var value = type switch
            {
                { } t when t.EndsWith("Flags") => $"({t}) {constant.Value}",
                { } t when t == "string" => "\"" + constant.Value + "\"",
                _ => constant.Value
            };

            return $"public static {type} {constant.ManagedName} = {value};\r\n";
        }
    }
}
