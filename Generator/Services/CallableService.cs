using System;
using System.Collections.Generic;
using Repository.Model;

namespace Generator.Services
{
    public static class CallableService
    {
        public static string WriteReturnValue(Callback callback)
            => callback.ReturnValue.Type.ToString();
        
        public static string WriteParameters(Callback callback)
        {
            return "";
            // TODO: We will need some kind of parameter marshalling logic
            // This should take into account Type, Direction, Lifetime, Transfer,
            // Marshalling Behaviour (e.g. Value or Reference), and many other
            // factors. It could probably be its own dedicated service.

            /*
            List<Argument> args = callback.Arguments;
            
            var result = string.Empty;

            // We have no parameters -> Empty brackets
            if (args is null)
                return result;
            
            // We have parameters -> (Type1 name, Type2 name, ...)
            var first = true;
            foreach (Argument arg in args)
            {
                var name = arg.Name;
                var type = arg.Type.ToString();

                result += first ? $"{type} {name}" : $", {type} {name}";
                first = false;
            }

            return result;
            */
        }
    }
}
