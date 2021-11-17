namespace Generator3.Renderer.Internal
{
    public static class Method
    {
        public static string Render(this Model.Internal.Method? method)
        {
            return method is null 
                ? "" 
                : @$"{method.RenderComment()}
[DllImport(""{ method.NameSpaceName }"", EntryPoint = ""{ method.CIdentifier }"")]
public static extern { method.ReturnType.Render() } { method.Name }({ method.Parameters.Render()});";
        }

        private static string RenderComment(this Model.Internal.Method function) =>
$@"/// <summary>
/// Calls native method {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
