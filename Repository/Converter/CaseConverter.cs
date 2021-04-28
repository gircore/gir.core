using System.Linq;
using System.Text;

namespace Repository
{
    public class CaseConverter
    {
        public string ToCamelCase(string str)
        {
            var pascalCase = ToPascal(str);
            
            return str switch
            {
                { Length: 0 } => "",
                { Length: 1 } => pascalCase.ToLower(),
                _ => char.ToLower(pascalCase[0]) + pascalCase[1..]
            };
        }
        
        public string ToPascalCase(string str)
        {
            return str switch
            {
                { Length: 0 } => "",
                { Length: 1 } => str.ToUpper(),
                _ => ToPascal(str)
            };
        }

        public string ToPascal(string str)
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
}
