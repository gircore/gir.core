/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Type = System.Type;

namespace Generator3.Renderer.Public
{
    internal static class Method
    {
        private readonly Method _method;
        private readonly Namespace _currentNamespace;
        private readonly SymbolName _parent;

        private readonly IEnumerable<Parameter> _nativeParams;
        private readonly IEnumerable<SingleParameter> _managedParams;
        private readonly IEnumerable<SingleParameter> _nullParams;
        private readonly IEnumerable<SingleParameter> _delegateParams;
        private readonly IEnumerable<SingleParameter> _marshalParams;

        private readonly InstanceParameter? _instanceParameter;

        // We use a system of 'Blocks' to make sure resources are allocated and
        // deallocated in the correct order. Blocks are nested inside each other,
        // wrapping the inner blocks with a given 'start' and 'end' statement. This
        // enforces a system of FILO (first in, last out), which makes sure that
        // resources are not accidentally deleted while in-use.
        private readonly BlockStack _stack = new();

        public MethodGenerator(Method method, SymbolName parent_name, Namespace currentNamespace)
        {
            _method = method;
            _parent = parent_name;
            _currentNamespace = currentNamespace;

            _nativeParams = method.ParameterList.GetParameters();
            _managedParams = method.ParameterList.GetManagedParameters();
            _nullParams = method.ParameterList.SingleParameters.Except(_managedParams);
            _delegateParams = _managedParams.Where(arg => arg.TypeReference.GetResolvedType() is Callback);
            _marshalParams = _managedParams.Except(_delegateParams);
            _instanceParameter = method.ParameterList.InstanceParameter;
        }

        public static string Render(this Model.Public.Method method)
        {
            if (!method.CanRender())
                return string.Empty;

            try
            {
                var stack = new BlockStack();
                method.AddMethodSignature(stack);
                AddDelegateLifecycleManagement();
                AddParameterConversions();
                AddMethodCall();
                AddReturnValue(); // TODO: ReturnValue in wrong order

                return stack.Build();
            }
            catch (Exception e)
            {
                Log.Warning($"Did not generate method '{_parent}.{_method.Name}': {e.Message}");
                return string.Empty;
            }
        }

        private static bool CanRender(this Model.Public.Method method)
        {
            // We only support a subset of methods at the
            // moment. Determine if we can generate based on
            // the following criteria:

            if (method.IsFree())
                return false;

            if (method.HasInOutRefParameter())
                return false;

            if (method.HasCallbackReturnType())
                return false;

            if (method.HasUnionParameter())
                return false;

            if(method.HasUnionReturnType())
                return false;

            if(method.HasArrayClassParameter())
                return false;

            if(method.HasArrayClassReturnType())
                return false;

            return true;
        }

        private static void AddMethodSignature(this Model.Public.Method method, BlockStack blockStack)
        {
            var returnValue = method.ReturnType.Render();
            var parameters = method.Parameters.Render();
            var instanceParameter = method.InstanceParameter.Render();
            
            if(parameters.Length > 0)
                parameters = ", " + parameters;
            
            blockStack.Nest(new()
            {
                Start = $"public {returnValue} {method.Name}({instanceParameter} {parameters})\r\n{{",
                End = "}"
            });
        }

        private static void AddDelegateLifecycleManagement(this Model.Public.Method method, BlockStack blockStack)
        {
            // For each delegate-type parameter, we need to create a delegate
            // handling object. This ensures that the delegate and associated
            // resources are kept valid until the delegate is no longer needed
            // by the C-library.

            var delegateParams = method.Parameters.Where(param => param.IsCallback);

            foreach (var dlgParam in delegateParams)
            {
                var managedType = dlgParam.Render();

                var handlerType = dlgParam.CallbackScope switch
                {
                    Scope.Call => $"{managedType}CallHandler",
                    Scope.Async => $"{managedType}AsyncHandler",
                    Scope.Notified => $"{managedType}NotifiedHandler",
                    _ => throw new NotSupportedException($"{nameof(Scope)}: '{dlgParam.CallbackScope}' was not recognised")
                };

                var alloc = $"var {dlgParam.Name}Handler = new {handlerType}({dlgParam.Name});";

                blockStack.Nest(new Block()
                {
                    Start = alloc
                });
            }
        }

        private void AddParameterConversions()
        {
            if (_instanceParameter != null)
                AddParameterConversion(_instanceParameter);

            foreach (var parameter in _marshalParams)
                AddParameterConversion(parameter);
        }

        private void AddParameterConversion(Parameter parameter)
        {
            // Skip null parameters
            if (_nullParams.Contains(parameter))
                return;

            // If we are the instance parameter, marshal variable 'this'
            var fromParamName = parameter == _instanceParameter
                ? "this"
                : parameter.Name;

            var expression = Convert.ManagedToNative(
                transferable: parameter,
                fromParam: fromParamName,
                currentNamespace: _currentNamespace
            );

            // TODO: Use actual parameter type again: {parameter.WriteType(Target.Native, _currentNamespace)}
            var alloc = $"var {parameter.Name}Native = {expression};";
            var dealloc = $"// TODO: Free {parameter.Name}Native";

            _stack.Nest(new Block()
            {
                Start = alloc,
                End = dealloc
            });
        }

        private void AddMethodCall()
        {
            var call = new StringBuilder();

            if (!_method.ReturnValue.IsVoid())
                call.Append("var result = ");

            // TODO: Handle excluded items
            IEnumerable<string> args = _nativeParams.Select(arg =>
            {
                // TODO: Better type handling
                if (_nullParams.Contains(arg))
                    return "IntPtr.Zero";

                // Do something
                Type type = arg.TypeReference.GetResolvedType();
                var argText = type switch
                {
                    Callback => $"{arg.Name}Handler.NativeCallback",
                    _ => $"{arg.Name}Native"
                };

                return argText;
            });

            call.Append($"Native.{_parent}.Instance.Methods.{_method.Name}(");
            call.Append(string.Join(", ", args));
            call.Append(");\n");

            _stack.Nest(new Block()
            {
                Start = call.ToString()
            });
        }

        private void AddReturnValue()
        {
            if (_method.ReturnValue.IsVoid())
                return;

            var expression = Convert.NativeToManaged(
                transferable: _method.ReturnValue,
                fromParam: "result",
                currentNamespace: _currentNamespace
            );

            _stack.Nest(new()
            {
                Start = $"return {expression};"
            });
        }
    }
}
*/
