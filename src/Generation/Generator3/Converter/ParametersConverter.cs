using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator3.Converter
{
    public static class ParametersConverter
    {
        public static string Render(IEnumerable<Model.Public.Parameter> parameters, out IEnumerable<string> parameterNames)
        {
            var call = new StringBuilder();
            var names = new List<string>();

            foreach (var p in parameters.Select(x => x.Model))
            {
                var expression = p.ToNative(out var variableName);
                if (expression is not null)
                    call.AppendLine(expression);

                names.Add(variableName);
            }

            parameterNames = names;

            return call.ToString();
        }
    }
}
