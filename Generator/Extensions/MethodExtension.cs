using System;
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
            var methodText = $"public static extern {returnValue} {method.ManagedName}({method.Arguments.WriteNative(currentNamespace)});\r\n";

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
            if (method is null)
                return string.Empty;
            
            var builder = new StringBuilder();

            var delegateParams = method.Arguments.Where(arg => arg.SymbolReference.GetSymbol().GetType() == typeof(Callback));
            var marshalParams = method.Arguments.Except(delegateParams);
            var returnValue = method.ReturnValue;
            var isInstance = method.InstanceArgument != null;

            builder.AppendLine("// Method: " + method.ManagedName);
            builder.AppendLine("// IsInstance: " + isInstance);

            foreach (var arg in delegateParams)
                builder.AppendLine("// Delegate Arg: " + arg.ManagedName);
            
            foreach (var arg in marshalParams)
                builder.AppendLine("// Marshal Arg: " + arg.ManagedName);

            builder.AppendLine("// With Return Value: " + returnValue.WriteManaged(currentNamespace));

            if (!isInstance)
                goto exit;
            
            // TODO: REMOVE INSTANCE PARAMETER FROM ARGUMENTS
            builder.AppendLine($"public {returnValue.WriteManaged(currentNamespace)} {method.ManagedName}({method.Arguments.WriteManaged(currentNamespace)})");
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
                    Scope.Call => $"{symbol.ManagedName}CallHandler",
                    Scope.Async => $"{symbol.ManagedName}AsyncHandler",
                    Scope.Notified => $"{symbol.ManagedName}NotifiedHandler"
                };

                var alloc = $"var {dlgParam.ManagedName}Handler = new {handlerType}({dlgParam.ManagedName});";

                stack.Nest(new Block()
                {
                    Start = alloc
                });
            }
            
            stack.AddWhitespace();
            
            // 2. marshal parameters
            foreach (var arg in marshalParams)
            {
                Symbol symbol = arg.SymbolReference.GetSymbol();
                var alloc = arg.WriteMarshalArgumentToNative($"{arg.ManagedName}Native", arg.ManagedName, currentNamespace);
                var dealloc = $"// TODO: Free {arg.ManagedName}Native";
                
                stack.Nest(new Block()
                {
                    Start = alloc,
                    End = dealloc
                });
            }

            stack.AddWhitespace();
            
            // 3. method call
            stack.Nest(new Block()
            {
                Start = "// Do Native Call"
            });

            // The BlockStack then automatically inserts the cleanup code
            // in reverse order, making sure we free resources appropriately.
            var methodBody = stack.Build();
            builder.AppendLine(methodBody);
            
            // 4. (optional) return value
            
            builder.AppendLine("}");

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

            foreach (var argument in method.Arguments)
            {
                builder.AppendLine($"/// <param name=\"{argument.ManagedName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }
    }
}
