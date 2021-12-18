using System.Collections.Generic;
using System.Text;
using Generator3.Converter;

namespace Generator3.Generation.Callback
{
    public static class PublicHandlerConvertParameterStatements
    {
        public static string Render(PublicHandlerModel model, out IEnumerable<string> parameters)
        {
            var call = new StringBuilder();
            var names = new List<string>();
            
            foreach (var p in model.PublicParameters)
            {
                call.AppendLine(p.ToManaged(out var variableName));
                names.Add(variableName);
            }

            parameters = names;
            
            return call.ToString();
        }
    }
}
