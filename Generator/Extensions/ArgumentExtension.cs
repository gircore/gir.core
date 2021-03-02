using Repository.Model;

namespace Generator
{
    public static class ArgumentExtension
    {
        public static string WriteManaged(this Argument argument)
        {
            return "ArgumentWriteManaged";
        }
        
        public static string WriteNative(this Argument argument)
        {
            return "ArgumentWriteNative";
        }
    }
}
