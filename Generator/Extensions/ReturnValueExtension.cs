using Repository.Model;

namespace Generator
{
    public static class ReturnValueExtension
    {
        public static string WriteNative(this ReturnValue returnValue, Namespace currentNamespace)
        {
            var type = returnValue.SymbolReference.GetSymbol().NativeName;
            
            if (returnValue.SymbolReference.GetSymbol().Namespace is null || currentNamespace == returnValue.SymbolReference.GetSymbol().Namespace)
            {
                if(returnValue.Array is {})
                    return type + "[]";

                return type;
            }

            var namespaceName = returnValue.SymbolReference.GetSymbol().Namespace?.Name;
            
            if(returnValue.Array is {})
                return namespaceName + "." + type + "[]";

            return namespaceName + "." + type;
        }
        
        public static string WriteManaged(this ReturnValue returnValue)
        {
            return "ReturnValueManaged";
        }
    }
}
