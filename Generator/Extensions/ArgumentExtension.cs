using Repository.Model;

namespace Generator
{
    public static class ArgumentExtension
    {
        public static string WriteManaged(this Argument argument)
        {
            return "ArgumentWriteManaged";
        }
        
        public static string WriteNative(this Argument argument, Namespace currentNamespace)
        {
            var attribute = argument.Array.GetMarshallAttribute();
            var type = ((Type) argument).WriteNative(currentNamespace);

            if (attribute.Length > 0)
                attribute += " ";
            
            return $"{attribute}{type}";
        }
    }
}
