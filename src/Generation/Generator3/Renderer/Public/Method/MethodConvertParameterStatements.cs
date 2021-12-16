using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class MethodConvertParameterStatements
    {
        public static string Render(Model.Public.Method method, out IEnumerable<string> parameterNames)
        {
            var call = new StringBuilder();
            var names = new List<string>();

            foreach (var p in method.Parameters.Select(x => x.Model))
            {
                call.AppendLine(p.ToManaged(out var variableName));
                names.Add(variableName);
            }

            parameterNames = names;

            return call.ToString();
        }
    }
}
