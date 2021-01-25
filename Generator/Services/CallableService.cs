using Repository.Model;

namespace Generator.Services
{
    public static class CallableService
    {
        public static string WriteReturnValue(Callback callback)
            => callback.ReturnValue.Type.ToString();
        
        public static string WriteParameters(Callback callback)
        {
            // TODO: We will need some kind of parameter marshalling logic
            // This should take into account Type, Direction, Lifetime, Transfer,
            // Marshalling Behaviour (e.g. Value or Reference), and many other
            // factors. It could probably be its own dedicated service.
            
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
