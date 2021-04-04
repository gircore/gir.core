using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class MethodExtension
    {
        public static string WriteNative(this Method? method, Namespace currentNamespace)
        {
            if (method is null )
                return string.Empty;


            var returnValue = method.ReturnValue.WriteNative(currentNamespace);

            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{currentNamespace.Name}\", EntryPoint = \"{method.Name}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.SymbolName}({method.ParameterList.WriteNative(currentNamespace)});\r\n";

            return summaryText + dllImportText + methodText;
        }

        private record Block
        {
            public Block? Inner { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            
            public string Build()
                => Build(new StringBuilder()).ToString();

            private StringBuilder Build(StringBuilder builder)
            {
                builder.AppendLine(Start);
                Inner?.Build(builder);
                builder.AppendLine(End);
                return builder;
            }
        }

        private class BlockStack
        {
            private Block root;
            private Block latest;

            public void Nest(Block block)
            {
                if (root is null)
                {
                    root = block;
                    latest = root;
                }

                latest.Inner = block;
                latest = block;
            }

            public void AddWhitespace()
                => Nest(new Block());

            public string Build()
                => root.Build();
        }
        
        public static string WriteManaged(this Method? method, Namespace currentNamespace)
        {
            // TODO: Move these outside
            static string ArgToNative(Parameter arg, string toParamName, string fromParamName, Namespace currentNamespace)
            {
                var expression = Convert.ManagedToNative(
                    fromParam: fromParamName,
                    symbol: arg.SymbolReference.GetSymbol(),
                    typeInfo: arg.TypeInformation,
                    currentNamespace: currentNamespace
                );
            
                return $"{arg.WriteType(Target.Native, currentNamespace)} {toParamName} = {expression};";
            }
            
            static string ArgToManaged(Parameter arg, string toParamName, string fromParamName, Namespace currentNamespace)
            {
                var expression = Convert.NativeToManaged(
                    fromParam: fromParamName,
                    symbol: arg.SymbolReference.GetSymbol(),
                    typeInfo: arg.TypeInformation,
                    currentNamespace: currentNamespace,
                    transfer: arg.Transfer
                );
            
                return $"{arg.WriteType(Target.Managed, currentNamespace)} {toParamName} = {expression};";
            }
            
            if (method is null)
                return string.Empty;
            
            var builder = new StringBuilder();

            // The 'true' arguments of the method that we pass to our wrapped call
            IEnumerable<Parameter> nativeParams = method.ParameterList.GetParameters();

            // The arguments used in the managed method signature (e.g. no userData)
            IEnumerable<SingleParameter> managedParams = method.ParameterList.SingleParameters;
            
            // Instance argument (which we want to exclude from the managed signature)
            InstanceParameter? instanceArg = method.ParameterList.InstanceParameter;

            // Delegate-type arguments only
            IEnumerable<SingleParameter> delegateParams = managedParams
                .Where(arg => arg.SymbolReference.GetSymbol().GetType() == typeof(Callback));
            
            // All other arguments
            IEnumerable<SingleParameter> marshalParams = managedParams.Except(delegateParams);
            
            // Method return value
            ReturnValue returnValue = method.ReturnValue;
            
            
            
            // Misc Logging
            var isInstance = method.ParameterList.InstanceParameter != null;

            builder.AppendLine("// Method: " + method.SymbolName);
            builder.AppendLine("// IsInstance: " + isInstance);

            foreach (var arg in delegateParams)
                builder.AppendLine("// Delegate Arg: " + arg.SymbolName);
            
            foreach (var arg in marshalParams)
                builder.AppendLine("// Marshal Arg: " + arg.SymbolName);

            builder.AppendLine("// With Return Value: " + returnValue.WriteManaged(currentNamespace));

            // We don't support this (static generation) yet
            if (!isInstance)
                goto exit;
            
            // TODO: REMOVE INSTANCE PARAMETER FROM ARGUMENTS
            builder.AppendLine($"public {returnValue.WriteManaged(currentNamespace)} {method.SymbolName}({method.ParameterList.WriteManaged(currentNamespace)})");
            builder.AppendLine("{");
            
            // TODO: Method Generation
            // We use a system of 'Blocks' to make sure resources are allocated and
            // deallocated in the correct order. Blocks are nested inside each other,
            // wrapping the inner blocks with a given 'start' and 'end' statement. This
            // enforces a system of FILO (first in, last out), which makes sure that
            // resources are not accidentally deleted while in-use.
            var stack = new BlockStack();
            
            // 1. delegate setup
            foreach (var dlgParam in delegateParams)
            {
                Symbol symbol = dlgParam.SymbolReference.GetSymbol();
                var handlerType = dlgParam.CallbackScope switch
                {
                    Scope.Call => $"{symbol.Namespace.Name}.{symbol.SymbolName}CallHandler",
                    Scope.Async => $"{symbol.Namespace.Name}.{symbol.SymbolName}AsyncHandler",
                    Scope.Notified => $"{symbol.Namespace.Name}.{symbol.SymbolName}NotifiedHandler"
                };

                var alloc = $"var {dlgParam.SymbolName}Handler = new {handlerType}({dlgParam.SymbolName});";

                stack.Nest(new Block()
                {
                    Start = alloc
                });
            }
            
            stack.AddWhitespace();
            
            // 2. instance parameter
            if (instanceArg is { })
            {
                Symbol symbol = instanceArg.SymbolReference.GetSymbol();
                var alloc = ArgToNative(instanceArg, $"{instanceArg.SymbolName}Native", "this", currentNamespace);
                var dealloc = $"// TODO: Free {instanceArg.SymbolName}Native (this)";
                
                stack.Nest(new Block()
                {
                    Start = alloc,
                    End = dealloc
                });
            }
            
            // 3. marshal parameters
            foreach (var arg in marshalParams)
            {
                Symbol symbol = arg.SymbolReference.GetSymbol();
                var alloc = ArgToNative(arg, $"{arg.SymbolName}Native", arg.SymbolName, currentNamespace);
                var dealloc = $"// TODO: Free {arg.SymbolName}Native";
                
                stack.Nest(new Block()
                {
                    Start = alloc,
                    End = dealloc
                });
            }

            stack.AddWhitespace();
            
            // 4. method call
            {
                var call = new StringBuilder();

                if (!returnValue.IsVoid())
                    call.Append("var result = ");
                
                // TODO: Handle excluded items
                IEnumerable<string> args = nativeParams.Select(arg =>
                {
                    // Do something
                    Symbol symbol = arg.SymbolReference.GetSymbol();
                    var argText = symbol switch
                    {
                        Callback => $"{arg.SymbolName}Handler.NativeCallback",
                        _ => $"{arg.SymbolName}Native"
                    };

                    return argText;
                });

                call.Append($"Native.Instance.Methods.{method.SymbolName}(");
                call.Append(string.Join(", ", args));
                call.Append(");\n");
                
                stack.Nest(new Block()
                {
                    Start = call.ToString()
                });    
            }
            

            // The BlockStack then automatically inserts the cleanup code
            // in reverse order, making sure we free resources appropriately.
            var methodBody = stack.Build();
            
            // Indent appropriately (by 4 spaces)
            var indent = "    ";
            builder.Append(methodBody.Replace("\n", "\n" + indent));
            
            // 5. (optional) return value
            if (!returnValue.IsVoid())
            {
                var expression = Convert.NativeToManaged(
                    fromParam: "result",
                    symbol: returnValue.SymbolReference.GetSymbol(),
                    typeInfo: returnValue.TypeInformation,
                    currentNamespace: currentNamespace,
                    transfer: returnValue.Transfer
                );
                
                builder.AppendLine($"return {expression};");   
            }

            builder.AppendLine("\n}");

            exit: // <-- TODO: Avoid labels. Not very idiomatic
            builder.Append("\n\n\n");
            return builder.ToString();
        }
        
        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Calls native method {method.Name}.");
            builder.AppendLine($"/// </summary>");

            foreach (var argument in method.ParameterList.GetParameters())
            {
                builder.AppendLine($"/// <param name=\"{argument.SymbolName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }
    }
}
