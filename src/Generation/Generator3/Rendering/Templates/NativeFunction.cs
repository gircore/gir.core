using System.Linq;

namespace Generator3.Rendering.Templates
{
    public static class NativeFunction
    {
        public static string Get(Generation.Model.NativeFunction nativeFunction) =>
@$"{GetComments(nativeFunction)}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { ReturnType.Get(nativeFunction.ReturnType) } { nativeFunction.Name }({ string.Join(", ", nativeFunction.Parameters.Select(Parameter.Get)) });";


        private static string GetComments(Generation.Model.NativeFunction nativeFunction) =>
$@"/// <summary>
/// Calls native method {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.Select(x => x.Model).Write(GetParameterSummary)}
{GetReturnValueSaummary(nativeFunction.Model.ReturnType)}";

        private static string GetReturnValueSaummary(GirModel.ReturnType returnType) =>
            $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";

        private static string GetParameterSummary(GirModel.Parameter parameter) =>
            $@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";
    }
}
