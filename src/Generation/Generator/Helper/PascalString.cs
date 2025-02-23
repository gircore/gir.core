using System.Linq;
using System.Text;

internal static class PascalString
{
    public static string ToPascalCase(this string str)
    {
        return str switch
        {
            { Length: 0 } => string.Empty,
            { Length: 1 } => str.ToUpper(),
            _ => ToPascal(str)
        };
    }

    private static string ToPascal(this string str)
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
