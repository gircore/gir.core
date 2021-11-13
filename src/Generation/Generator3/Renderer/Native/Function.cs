namespace Generator3.Renderer.Native
{
    public static class Function
    {
        public static string Render(this Model.Native.Function? nativeFunction)
        {
            if (nativeFunction is null)
                return "";

            try
            {
                return nativeFunction.RenderFunction();
            }
            catch
            {
                Log.Warning($"Could not render native function \"{nativeFunction.CIdentifier}\".");
                throw;
            }
        }

        private static string RenderFunction(this Model.Native.Function function)
            => @$"{function.RenderComment()}
[DllImport(""{ function.NameSpaceName }"", EntryPoint = ""{ function.CIdentifier }"")]
public static extern { function.ReturnType.Render() } { function.Name }({ function.Parameters.Render()});";

        private static string RenderComment(this Model.Native.Function function) =>
$@"/// <summary>
/// Calls native function {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
