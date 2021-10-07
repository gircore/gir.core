using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator3.Generation.Code
{
    public class NativeFunction
    {
        private readonly GirModel.Method _function;
        private IEnumerable<Parameter>? _parameters;
        private ReturnValue? _returnValue;

        public string Name => _function.Name;

        public string ReturnTypeName
        {
            get
            {
                _returnValue ??= ReturnValue.CreateNative(_function.ReturnValue);
                return _returnValue.Code;
            }
        }
        public string CIdentifier => _function.CIdentifier;
        public string NameSpaceName => _function.NamespaceName;

        public IEnumerable<Parameter> Parameters => _parameters ??= _function.Parameters.Select(Parameter.CreateNative);
        
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

            return summary 
                   + Environment.NewLine 
                   + string.Join(Environment.NewLine, _function.Parameters.Select(GetParameterSummary))
                   + Environment.NewLine
                   + GetReturnValueSaummary();
        }

        private string GetReturnValueSaummary() =>
            $@"/// <returns>Transfer ownership: {_function.ReturnValue.Transfer} Nullable: {_function.ReturnValue.Nullable}</returns>";

        private string GetParameterSummary(GirModel.Parameter parameter) =>
$@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";
    }
}
