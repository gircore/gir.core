using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GirModel;

namespace Generator3.Renderer.Public
{
    internal static class Method
    {
        public static string Render(this Model.Public.Method method)
        {
            if (!method.CanRender())
                return string.Empty;

            try
            {
                var stack = new BlockStack();
                stack.AddMethodSignature(method);
                stack.AddDelegateLifecycleManagement(method);
                //AddParameterConversions();
                stack.AddMethodCall(method);
                stack.AddReturnValue(method);

                return stack.Build();
            }
            catch (Exception e)
            {
                Log.Warning($"Did not generate method '{method.ClassName}.{method.Name}': {e.Message}");
                return string.Empty;
            }
        }

        private static bool CanRender(this Model.Public.Method method)
        {
            // We only support a subset of methods at the
            // moment. Determine if we can generate based on
            // the following criteria:

            if (method.Parameters.Any())
                return false;
            
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

        private static void AddMethodSignature(this BlockStack blockStack, Model.Public.Method method)
        {
            var returnValue = method.PublicReturnType.Render();
            var parameters = method.Parameters.Render();
            var instanceParameter = method.InstanceParameter.Render();
            
            if(parameters.Length > 0)
                parameters = ", " + parameters;
            
            blockStack.Nest(new()
            {
                Start = $"public {returnValue} {method.Name}({parameters})\r\n{{",
                End = "}"
            });
        }

        private static void AddDelegateLifecycleManagement(this BlockStack blockStack, Model.Public.Method method)
        {
            // For each delegate-type parameter, we need to create a delegate
            // handling object. This ensures that the delegate and associated
            // resources are kept valid until the delegate is no longer needed
            // by the C-library.

            var delegateParams = method.Parameters.Where(param => param.IsCallback);

            foreach (var dlgParam in delegateParams)
            {
                var managedType = dlgParam.Render();

                var handlerType = dlgParam.Scope switch
                {
                    Scope.Call => $"{managedType}CallHandler",
                    Scope.Async => $"{managedType}AsyncHandler",
                    Scope.Notified => $"{managedType}NotifiedHandler",
                    _ => throw new NotSupportedException($"{nameof(Scope)}: '{dlgParam.Scope}' was not recognised")
                };

                var alloc = $"var {dlgParam.Name}Handler = new {handlerType}({dlgParam.Name});";

                blockStack.Nest(new Block()
                {
                    Start = alloc
                });
            }
        }

        /*private void AddParameterConversions()
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
        }*/

        private static void AddMethodCall(this BlockStack blockStack, Model.Public.Method method)
        {
            var call = new StringBuilder();

            if (!method.PublicReturnType.IsVoid())
                call.Append("var result = ");

            // TODO: Handle excluded items
            IEnumerable<string> parameters = method.Parameters.Select(arg => arg.IsCallback
                ? $"{arg.Name}Handler.NativeCallback"
                : $"{arg.Name}Native");

            call.Append($"Internal.{method.ClassName}.Instance.Methods.{method.Name}(");
            call.Append("this.Handle" + (parameters.Any() ? "," : string.Empty));
            call.Append(string.Join(", ", parameters));
            call.Append(");\n");

            blockStack.Nest(new Block()
            {
                Start = call.ToString()
            });
        }

        private static void AddReturnValue(this BlockStack blockStack, Model.Public.Method method)
        {
            if (method.PublicReturnType.IsVoid())
                return;

            blockStack.Nest(new()
            {
                Start = $"throw new System.Exception(); //return {method.InternalReturnType.RenderTo(method.PublicReturnType)};"
            });
        }
    }
}
