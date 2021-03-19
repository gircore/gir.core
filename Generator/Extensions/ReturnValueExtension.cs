using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class ReturnValueExtension
    {
        public static bool IsVoid(this ReturnValue returnValue)
            => returnValue.SymbolReference.GetSymbol().SymbolName == "void";

        public static string WriteNative(this ReturnValue returnValue, Namespace currentNamespace)
        {
            return returnValue.WriteType(Target.Native, currentNamespace);
        }

        public static string WriteManaged(this ReturnValue returnValue, Namespace currentNamespace)
        {
            return returnValue.WriteType(Target.Managed, currentNamespace);
        }
    }
}
