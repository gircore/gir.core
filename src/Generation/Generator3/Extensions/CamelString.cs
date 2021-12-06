namespace Generator3.Extensions
{
    public static class CamelString
    {
        public static string ToCamelCase(this string str)
        {
            var pascalCase = str.ToPascal();

            return str switch
            {
                { Length: 0 } => string.Empty,
                { Length: 1 } => pascalCase.ToLower(),
                _ => char.ToLower(pascalCase[0]) + pascalCase[1..]
            };
        }
    }
}
