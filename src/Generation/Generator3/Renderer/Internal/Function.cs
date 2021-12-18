namespace Generator3.Renderer.Internal
{
    public static class Function
    {
        public static string Render(this Model.Internal.Function? function)
        {
            if (function is null)
                return "";

            try
            {
                return function.RenderFunction();
            }
            catch
            {
                Log.Warning($"Could not render internal function \"{function.CIdentifier}\".");
                throw;
            }
        }

        private static string RenderFunction(this Model.Internal.Function function)
            => @$"{function.RenderComment()}
[DllImport(""{ function.NameSpaceName }"", EntryPoint = ""{ function.CIdentifier }"")]
public static extern { function.ReturnType.NullableTypeName } { function.Name }({ function.Parameters.Render()});";

        private static string RenderComment(this Model.Internal.Function function) =>
$@"/// <summary>
/// Calls native function {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
