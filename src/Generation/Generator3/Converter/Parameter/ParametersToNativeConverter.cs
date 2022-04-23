using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator3.Converter
{
    public static class ParametersToNativeConverter
    {
        public static string RenderToNative(IEnumerable<Model.Public.Parameter> parameters, out IEnumerable<string> parameterNames)
        {
            var expressions = new StringBuilder();
            var names = new List<string>();

            foreach (var p in parameters.Select(x => x.Model))
            {
                var expression = p.ToNative(out var variableName);
                if (expression is not null)
                    expressions.AppendLine(expression);

                names.Add(variableName);
            }

            parameterNames = names;

            return expressions.ToString();
        }
    }
}
