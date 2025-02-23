using System.Linq;
using System.Text;

internal static class CamelString
{
    public static string ToCamelCase(this string str)
    {
        var pascalCase = str.ToCamel();

        return str switch
        {
            { Length: 0 } => string.Empty,
            { Length: 1 } => pascalCase.ToLower(),
            _ => char.ToLower(pascalCase[0]) + pascalCase[1..]
        };
    }

    private static string ToCamel(this string str)
    {
        var words = str.Replace("_", "-").Split("-");
        var builder = new StringBuilder();
        foreach (var word in words.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            builder
                .Append(char.ToUpper(word[0]))
                .Append(word[1..]);
        }

        return builder.ToString();
    }
}
