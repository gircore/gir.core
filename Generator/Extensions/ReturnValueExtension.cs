using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    public static class ReturnValueExtension
    {
        public static string WriteNative(this ReturnValue returnValue, Namespace currentNamespace)
        {
            return ((Type) returnValue).WriteNative(currentNamespace);
        }

        public static string WriteManaged(this ReturnValue returnValue)
        {
            return "ReturnValueManaged";
        }
    }
}
