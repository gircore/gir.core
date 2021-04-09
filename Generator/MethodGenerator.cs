using Repository.Model;

namespace Generator
{
    internal partial class MethodGenerator
    {
        private readonly Method _method;
        private readonly Namespace _currentNamespace;
        private BlockStack _stack = new ();

        public MethodGenerator(Method method, Namespace currentNamespace)
        {
            _method = method;
            _currentNamespace = currentNamespace;
        }
        
        public string Generate()
        {
            AddMethodSignature();
            AddParameterConversions();
            AddMethodCall();
            AddReturnValue();
            
            return _stack.Build();
        }

        private void AddMethodSignature()
        {
            var returnValue = _method.ReturnValue.WriteManaged(_currentNamespace);
            var parameterList = _method.ParameterList.WriteManaged(_currentNamespace);
            _stack.Nest(new ()
            {
                Start = $"/*public {returnValue} {_method.SymbolName}({parameterList})\r\n{{",
                End = "}}*/"
            });
        }

        private void AddParameterConversions()
        {
            foreach (var parameter in _method.ParameterList.GetParameters())
                AddParameterConversion(parameter);
        }

        private void AddParameterConversion(Parameter parameter)
        {
            
        }

        private void AddMethodCall()
        {
            
        }

        private void AddReturnValue()
        {
            if (_method.ReturnValue.IsVoid())
                return;

            var expression = Convert.NativeToManaged(
                fromParam: "result",
                symbol: _method.ReturnValue.SymbolReference.GetSymbol(),
                typeInfo: _method.ReturnValue.TypeInformation,
                currentNamespace: _currentNamespace,
                transfer: _method.ReturnValue.Transfer
            );
            
            _stack.Nest(new()
            {
                Start = $"return {expression};"
            });
        }
    }
}
