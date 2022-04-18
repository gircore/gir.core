using System;

namespace Generator3.Renderer.Internal
{
    public static class Method
    {
        public static string Render(this Model.Internal.Method? method)
        {
            if (method is null)
                return string.Empty;

            if (method.Name == method.ClassName)
            {
                Log.Warning($"Method {method.Name} is named like its containing class. This is not allowed. The method should be created with a suffix and be rewritten to it's original name");
                return string.Empty;
            }

            var renderedParameters = method.Parameters.Render();

            if (renderedParameters.Length > 0)
                renderedParameters = ", " + renderedParameters;

            return @$"{method.RenderComment()}
[DllImport(ImportResolver.Library, EntryPoint = ""{ method.CIdentifier }"")]
public static extern { method.ReturnType.NullableTypeName } { method.Name }({method.InstanceParameter.Render()}{ renderedParameters });";
        }

        private static string RenderComment(this Model.Internal.Method function) =>
$@"/// <summary>
/// Calls native method {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
