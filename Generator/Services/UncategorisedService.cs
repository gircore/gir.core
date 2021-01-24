using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;

namespace Generator.Services
{
    // Miscellaneous Services
    [Obsolete("Purely for testing - incrementally restructure")]
    public class UncategorisedService : Service
    {
        public string WriteReturnValue(Callback callback)
            => callback.ReturnValue.Type.ToString();
        
        public string WriteParameters(Callback callback)
        {
            // List<ParameterInfo> parameters = delegateInfo?.Parameters?.Parameters;
            //
            // // We have no parameters -> Empty brackets
            // if (parameters is null)
            //     return string.Empty;
            //
            // var first = true;
            // var result = string.Empty;
            // foreach (ParameterInfo param in parameters)
            // {
            //     var name = param.Name;
            //     var type = ResolveType(param);
            //     result += first ? $"{type} {name}" : $", {type} {name}";
            //     first = false;
            // }

            return "some parameters";
        }

        public static string PrintTypeIdentifier(TypeReference type)
        {
            // External Type
            if (type.IsForeign)
                return $"{type.Type.Namespace}.{type.Type.ManagedName}";

            // Internal Type
            return type.Type.ManagedName;
        }
    }
}
