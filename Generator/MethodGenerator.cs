using System.Collections.Generic;
using System.Linq;
using Repository.Model;

namespace Generator
{
    internal partial class MethodGenerator
    {
        private readonly Method _method;
        private readonly Namespace _currentNamespace;
        private BlockStack _stack = new ();

        private IEnumerable<Parameter> nativeParams;
        private IEnumerable<SingleParameter> managedParams;
        private IEnumerable<SingleParameter> nullParams;
        private IEnumerable<SingleParameter> delegateParams;
        private IEnumerable<SingleParameter> marshalParams;

        public MethodGenerator(Method method, Namespace currentNamespace)
        {
            _method = method;
            _currentNamespace = currentNamespace;

            nativeParams = method.ParameterList.GetParameters();
            managedParams = method.ParameterList.GetManagedParameters();
            nullParams = method.ParameterList.SingleParameters.Except(managedParams);
            delegateParams = managedParams.Where(arg => arg.SymbolReference.GetSymbol().GetType() == typeof(Callback));
            marshalParams = managedParams.Except(delegateParams);
        }
        
        public string Generate()
        {
            AddMethodSignature();
            AddDelegateLifecycleManagement();
            AddParameterConversions();
            AddMethodCall();
            AddReturnValue();
            
            return _stack.Build();
        }

        private void AddDelegateLifecycleManagement()
        {
            // For each delegate-type parameter, we need to create a delegate
            // handling object. This ensures that the delegate and associated
            // resources are kept valid until the delegate is no longer needed
            // by the C-library.
            
            foreach (var dlgParam in delegateParams)
            {
                Symbol symbol = dlgParam.SymbolReference.GetSymbol();
                var managedName = symbol.Metadata["ManagedName"].ToString();
                
                var handlerType = dlgParam.CallbackScope switch
                {
                    Scope.Call => $"{symbol.Namespace.Name}.{managedName}CallHandler",
                    Scope.Async => $"{symbol.Namespace.Name}.{managedName}AsyncHandler",
                    Scope.Notified => $"{symbol.Namespace.Name}.{managedName}NotifiedHandler"
                };

                var alloc = $"var {dlgParam.SymbolName}Handler = new {handlerType}({dlgParam.SymbolName});";

                _stack.Nest(new Block()
                {
                    Start = alloc
                });
            }
        }

        private void AddMethodSignature()
        {
            var returnValue = _method.ReturnValue.WriteManaged(_currentNamespace);
            var parameterList = _method.ParameterList.WriteManaged(_currentNamespace);
            _stack.Nest(new ()
            {
                Start = $"public {returnValue} {_method.SymbolName}({parameterList})\r\n{{",
                End = "}"
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
