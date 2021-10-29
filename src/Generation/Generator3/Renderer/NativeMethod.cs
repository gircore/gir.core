using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeMethod
    {
        public static string Render(this Model.NativeMethod? nativeFunction)
        {
            return nativeFunction is null 
                ? "" 
                : @$"{nativeFunction.RenderComment()}
[DllImport(""{ nativeFunction.NameSpaceName }"", EntryPoint = ""{ nativeFunction.CIdentifier }"")]
public static extern { nativeFunction.ReturnType.Render() } { nativeFunction.Name }({ nativeFunction.Parameters.Render()});";
        }

        private static string RenderComment(this Model.NativeMethod nativeFunction) =>
$@"/// <summary>
/// Calls native method {nativeFunction.CIdentifier}.
/// </summary>
{nativeFunction.Parameters.RenderComment()}
{nativeFunction.Model.ReturnType.RenderComment()}";

        private static string RenderComment(this GirModel.ReturnType returnType) =>
            $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";

        private static string RenderComment(this IEnumerable<Model.Parameter> parameters)
        {
            return parameters
                .Select(GetParameterSummary)
                .Join(Environment.NewLine);
        }
        
        private static string GetParameterSummary(Model.Parameter parameter) =>
            $@"/// <param name=""{parameter.Model.Name}"">Transfer ownership: {parameter.Model.Transfer} Nullable: {parameter.Model.Nullable}</param>";
    }
}
