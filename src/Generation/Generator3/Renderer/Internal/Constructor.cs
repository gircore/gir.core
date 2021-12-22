namespace Generator3.Renderer.Internal
{
    public static class Constructor
    {
        public static string Render(this Model.Internal.Constructor? constructor)
        {
            return constructor is null
                ? ""
                : @$"{constructor.RenderComment()}
[DllImport(""{ constructor.NamespaceName }"", EntryPoint = ""{ constructor.CIdentifier }"")]
public static extern { constructor.ReturnType.NullableTypeName } { constructor.Name }({ constructor.Parameters.Render()});";
        }

        private static string RenderComment(this Model.Internal.Constructor constructor) =>
$@"/// <summary>
/// Calls native constructor {constructor.CIdentifier}.
/// </summary>
{constructor.Parameters.RenderComments()}
{constructor.Model.ReturnType.RenderComment()}";
    }
}
