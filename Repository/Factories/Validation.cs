using System.Text.RegularExpressions;

namespace Repository.Factories
{
    public static class Validation
    {
        public static string EscapeIdentifier(string ident)
        {
            // Member names may not start with a number, so prefix with an underscore
            if (Regex.IsMatch(ident, @"^\d"))
                ident = $"_{ident}";

            return ident;
        }
    }
}
