namespace Generator3.Renderer.Internal
{
    public static class Method
    {
        public static string Render(this Model.Internal.Method? method)
        {
            if (method is null)
                return "";
            
            var renderedParameters = method.Parameters.RenderWithAttributes();
            
            if(renderedParameters.Length > 0)
                renderedParameters = ", " + renderedParameters;
            
            
            return @$"{method.RenderComment()}
[DllImport(""{ method.NamespaceName }"", EntryPoint = ""{ method.CIdentifier }"")]
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
