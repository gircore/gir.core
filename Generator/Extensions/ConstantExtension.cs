using Repository.Model;

namespace Generator
{
    internal static class ConstantExtension
    {
        public static string WriteManaged(this Constant constant)
        {
            var type = constant.SymbolReference.GetSymbol().SymbolName;

            var value = type switch
            {
                { Value: { } t } when t.EndsWith("Flags") => $"({t}) {constant.Value}",
                { Value: { } t } when t == "string" => "\"" + constant.Value + "\"",
                _ => constant.Value
            };

            return $"public static {type} {constant.SymbolName} = {value};\r\n";
        }
    }
}
