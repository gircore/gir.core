using System.Text;
using Gir.Output.Model;

namespace Generator
{
    public static class ElementExtension
    {
        public static string WriteNativeSummary(this Element symbol)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Name: {symbol.Name}.");
            builder.AppendLine($"/// </summary>");
            return builder.ToString();
        }
    }
}
