namespace Generator3.Renderer
{
    public static class NativeMethod
    {
        public static string Render(this Model.NativeMethod? nativeFunction)
        {
            return nativeFunction is null 
                ? "" 
                : @$"{nativeFunction.RenderComment()}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnType.Render() } { nativeFunction.Name }({ nativeFunction.Parameters.Render()});";
        }

        private static string RenderComment(this Model.NativeMethod nativeFunction) =>
$@"/// <summary>
/// Calls native method {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.RenderComments()}
{nativeFunction.Model.ReturnType.RenderComment()}";
    }
}
