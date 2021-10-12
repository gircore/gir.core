using System.Linq;

namespace Generator3.Rendering.Templates
{
    public static class NativeFunction
    {
        public static string Get(Generation.Model.NativeFunction nativeFunction) =>
@$"{GetComments(nativeFunction)}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnTypeName } { nativeFunction.Name }({ string.Join(", ", nativeFunction.Parameters.Select(x => x.Code)) });";


        private static string GetComments(Generation.Model.NativeFunction nativeFunction) =>
$@"/// <summary>
/// Calls native method {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.Select(x => x.Model).Write(GetParameterSummary)}
{GetReturnValueSaummary(nativeFunction.Model.ReturnValue)}";

        private static string GetReturnValueSaummary(GirModel.ReturnValue returnValue) =>
            $@"/// <returns>Transfer ownership: {returnValue.Transfer} Nullable: {returnValue.Nullable}</returns>";

        private static string GetParameterSummary(GirModel.Parameter parameter) =>
            $@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";
    }
}
