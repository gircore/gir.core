using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeFunction
    {
        public static string Render(this Model.NativeFunction nativeFunction)
        {
            return @$"{GetComments(nativeFunction)}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnType.Render() } { nativeFunction.Name }({ nativeFunction.Parameters.Select(Parameter.Render).Join(", ") });";
        }

        private static string GetComments(Model.NativeFunction nativeFunction) =>
$@"/// <summary>
/// Calls native method {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.Select(x => x.Model).ForEachCall(GetParameterSummary)}
{GetReturnValueSaummary(nativeFunction.Model.ReturnType)}";

        private static string GetReturnValueSaummary(GirModel.ReturnType returnType) =>
            $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";

        private static string GetParameterSummary(GirModel.Parameter parameter) =>
            $@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";
    }
}
