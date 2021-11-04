namespace Generator3.Renderer
{
    public static class NativeFunction
    {
        public static string Render(this Model.NativeFunction? nativeFunction)
        {
            return nativeFunction is null 
                ? "" 
                : @$"{nativeFunction.RenderComment()}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnType.Render() } { nativeFunction.Name }({ nativeFunction.Parameters.Render()});";
        }

        private static string RenderComment(this Model.NativeFunction nativeFunction) =>
$@"/// <summary>
/// Calls native function {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.RenderComments()}
{nativeFunction.Model.ReturnType.RenderComment()}";
    }
}
