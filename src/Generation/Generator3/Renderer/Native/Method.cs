namespace Generator3.Renderer.Native
{
    public static class Method
    {
        public static string Render(this Model.Native.Method? nativeFunction)
        {
            return nativeFunction is null 
                ? "" 
                : @$"{nativeFunction.RenderComment()}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnType.Render() } { nativeFunction.Name }({ nativeFunction.Parameters.Render()});";
        }

        private static string RenderComment(this Model.Native.Method function) =>
$@"/// <summary>
/// Calls native method {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
