using System;
using System.Collections.Generic;
using Generator.Analysis;
using Generator.Introspection;

namespace Generator.Services
{
    // Miscellaneous Services
    [Obsolete("Purely for testing - incrementally restructure")]
    public class UncategorisedService : Service
    {
        public string WriteReturnValue(DelegateSymbol delegateSymbol, string nspace)
        {
            var returnValType = delegateSymbol.DelegateInfo.ReturnValue.Type;
            ISymbolInfo returnValSymbol = TypeDict.GetSymbol(nspace, returnValType.Name);
            return returnValSymbol.ManagedName.ToString();
        }
        
        public string WriteParameters(DelegateSymbol delegateSymbol)
        {
            CallbackInfo delegateInfo = delegateSymbol.DelegateInfo;
            List<ParameterInfo> parameters = delegateInfo?.Parameters?.Parameters;

            // We have no parameters -> Empty brackets
            if (parameters is null)
                return string.Empty;

            var first = true;
            var result = string.Empty;
            foreach (ParameterInfo param in parameters)
            {
                var name = param.Name;
                var type = ResolveType(delegateSymbol.ManagedName.Namespace, param);
                result += first ? $"{type} {name}" : $", {type} {name}";
                first = false;
            }

            return result;
        }

        public string ResolveType(string nspace, IType type)
        {
            // TODO: Replace with complex type resolution logic
            if (type.Array != null)
            {
                ISymbolInfo symbolInfo  = TypeDict.GetSymbol(nspace, type.Array.Type.Name);
                return $"{symbolInfo.ManagedName}[]";
            }
            else
            {
                ISymbolInfo symbolInfo  = TypeDict.GetSymbol(nspace, type.Type.Name);
                return $"{symbolInfo.ManagedName}";
            }
        }
        
        // TODO: Move to Marshal Service
        public string MarshalParameter(ISymbolInfo symbolInfo)
        {
            return string.Empty;
        }
    }
}
