using System.Text;

namespace Repository
{
    public interface IPascalCaseConverter
    {
        string Convert(string str);
    }

    public class PascalCaseConverter : IPascalCaseConverter
    {
        public string Convert(string str)
        {
            static string ToPascalCase(string s)
            {
                var words = s.Replace("_", "-").Split("-");
                var builder = new StringBuilder();
                foreach (var word in words)
                {
                    builder
                        .Append(char.ToUpper(word[0]))
                        .Append(word, 1, word.Length - 1);
                }

                return builder.ToString();
            }

            return str switch
            {
                { Length: 0 } => "",
                { Length: 1 } => str.ToUpper(),
                _ => ToPascalCase(str)
            };
        }
    }
}
