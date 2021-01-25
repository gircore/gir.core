using Repository.Model;

namespace Generator.Services
{
    public static class CallableService
    {
        public static string WriteReturnValue(Callback callback)
            => callback.ReturnValue.Type.ToString();
        
        public static string WriteParameters(Callback callback)
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
    }
}
