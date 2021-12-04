using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class MethodInternalCall
    {
        public static string RenderInternalMethodCall(this Model.Public.Method method)
        {
            var call = new StringBuilder();

            if (!method.ReturnType.AnyType.Is<GirModel.Void>())
                call.Append("var result = ");

            // TODO: Handle excluded items
            IEnumerable<string> parameters = method.Parameters.Select(arg => arg.IsCallback
                ? $"{arg.Name}Handler.NativeCallback"
                : $"{arg.Name}Native");

            call.Append($"Internal.{method.ClassName}.Instance.Methods.{method.NativeName}(");
            call.Append("this.Handle" + (parameters.Any() ? "," : string.Empty));
            call.Append(string.Join(", ", parameters));
            call.Append(");\n");

            return call.ToString();
        }
    }
}
