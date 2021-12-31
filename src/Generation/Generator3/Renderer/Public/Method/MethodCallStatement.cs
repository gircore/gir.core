using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class MethodCallStatement
    {
        public static string Render(Model.Public.Method method, IEnumerable<string> parameterNames, out string resultVariableName)
        {
            resultVariableName = "result";
            var call = new StringBuilder();

            if (!method.ReturnType.AnyType.Is<GirModel.Void>())
                call.Append($"var {resultVariableName} = ");

            call.Append($"Internal.{method.ClassName}.Instance.Methods.{method.InternalName}(");
            call.Append("this.Handle" + (parameterNames.Any() ? ", " : string.Empty));
            call.Append(string.Join(", ", parameterNames));
            call.Append(");\n");

            return call.ToString();
        }
    }
}
