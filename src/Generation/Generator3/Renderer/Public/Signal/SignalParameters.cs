using System.Collections.Generic;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class SignalParameters
    {
        public static string RenderAsSignalParammeters(this IEnumerable<Model.Public.Parameter> parameters)
        {
            var sb = new StringBuilder();
            var index = 1; //Argument 0 is reserved

            foreach (var parameter in parameters)
            {
                sb.AppendLine(parameter.RenderAsSignalParammeter(index));
                index++;
            }

            return sb.ToString();
        }
    }
}
