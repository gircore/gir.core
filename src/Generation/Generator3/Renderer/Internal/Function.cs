using System;

namespace Generator3.Renderer.Internal
{
    public static class Function
    {
        public static string Render(this Model.Internal.Function? function)
        {
            if (function is null)
                return string.Empty;

            try
            {
                return function.RenderFunction();
            }
            catch (Exception ex)
            {
                Log.Warning($"Could not render internal function \"{function.CIdentifier}\": {ex.Message}");
                return string.Empty;
            }
        }

        private static string RenderFunction(this Model.Internal.Function function)
            => @$"{function.RenderComment()}
{function.PlatformDependent.RenderPlatformSupportAttributes()}
[DllImport(ImportResolver.Library, EntryPoint = ""{ function.CIdentifier }"")]
public static extern { function.ReturnType.NullableTypeName } { function.Name }({ function.Parameters.Render()});";

        private static string RenderComment(this Model.Internal.Function function) =>
$@"/// <summary>
/// Calls native function {function.CIdentifier}.
/// </summary>
{function.Parameters.RenderComments()}
{function.Model.ReturnType.RenderComment()}";
    }
}
