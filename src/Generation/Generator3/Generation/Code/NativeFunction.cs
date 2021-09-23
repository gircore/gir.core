using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation
{
    public class NativeFunction
    {
        private readonly GirModel.Method _function;
        private IEnumerable<NativeParameter>? _parameters;

        public string Name => _function.Name;
        public string ReturnTypeName => _function.ReturnType.GetName();
        public string CIdentifier => _function.CIdentifier;
        public string NameSpaceName => _function.NamespaceName;

        public IEnumerable<NativeParameter> Parameters => _parameters ??= _function.Parameters.Select(x => new NativeParameter(x));
        
        public string Code => GetCode();

        public NativeFunction(GirModel.Method function)
        {
            _function = function;
        }

        public string GetCode() =>
@$"{GetComments()}
[DllImport(""{ NameSpaceName }"", EntryPoint = ""{ CIdentifier }"")]
public static extern { ReturnTypeName } { Name }({ string.Join(", ", Parameters.Select(x => x.Code)) });";

        private string GetComments()
        {
            var summary = 
$@"/// <summary>
/// Calls native method {CIdentifier}.
/// </summary>";

            return summary + Environment.NewLine + string.Join(Environment.NewLine, _function.Parameters.Select(GetParameterSummary));

        }

        private string GetParameterSummary(GirModel.Parameter parameter) =>
$@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";
    }
}
