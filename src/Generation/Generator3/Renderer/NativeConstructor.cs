namespace Generator3.Renderer
{
    public static class NativeConstructor
    {
        public static string Render(this Model.NativeConstructor? nativeConstructor)
        {
            return nativeConstructor is null 
                ? "" 
                : @$"{nativeConstructor.RenderComment()}
[DllImport(""{ nativeConstructor.NameSpaceName }"", EntryPoint = ""{ nativeConstructor.CIdentifier }"")]
public static extern { nativeConstructor.ReturnType.Render() } { nativeConstructor.Name }({ nativeConstructor.Parameters.Render()});";
        }

        private static string RenderComment(this Model.NativeConstructor nativeConstructor) =>
$@"/// <summary>
/// Calls native constructor {nativeConstructor.CIdentifier}.
/// </summary>
{nativeConstructor.Parameters.RenderComments()}
{nativeConstructor.Model.ReturnType.RenderComment()}";
    }
}
