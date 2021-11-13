namespace Generator3.Renderer.Native
{
    public static class Constructor
    {
        public static string Render(this Model.Native.Constructor? nativeConstructor)
        {
            return nativeConstructor is null 
                ? "" 
                : @$"{nativeConstructor.RenderComment()}
[DllImport(""{ nativeConstructor.NameSpaceName }"", EntryPoint = ""{ nativeConstructor.CIdentifier }"")]
public static extern { nativeConstructor.ReturnType.Render() } { nativeConstructor.Name }({ nativeConstructor.Parameters.Render()});";
        }

        private static string RenderComment(this Model.Native.Constructor constructor) =>
$@"/// <summary>
/// Calls native constructor {constructor.CIdentifier}.
/// </summary>
{constructor.Parameters.RenderComments()}
{constructor.Model.ReturnType.RenderComment()}";
    }
}
