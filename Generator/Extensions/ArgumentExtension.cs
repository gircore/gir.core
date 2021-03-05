using Repository.Model;

namespace Generator
{
    internal static class ArgumentExtension
    {
        public static string WriteManaged(this Argument argument)
        {
            return "ArgumentWriteManaged";
        }
        
        public static string WriteNativeType(this Argument argument, Namespace currentNamespace)
        {
            var attribute = GetAttribute(argument);
            var direction = GetDirection(argument);
            var type = GetType(argument, currentNamespace);
            var nullable = GetNullable(argument);
            
            return $"{attribute}{direction}{type}{nullable}";
        }
        
        private static string GetAttribute(Argument argument)
        {
            var attribute = argument.Array.GetMarshallAttribute();
            
            if (attribute.Length > 0)
                attribute += " ";

            return attribute;
        }

        private static string GetDirection(Argument argument)
        {
            return argument.Direction switch
            {
                Direction.OutCalleeAllocates => "out ",
                Direction.OutCallerAllocates => "ref ",
                _ => ""
            };
        }
        
        private static string GetType(Argument argument, Namespace currentNamespace)
            => ((Type) argument).WriteNative(currentNamespace);
        
        private static string GetNullable(Argument argument)
            => argument.Nullable ? "?" : string.Empty;
    }
}
