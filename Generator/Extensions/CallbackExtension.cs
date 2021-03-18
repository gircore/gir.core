using Repository.Model;

namespace Generator
{
    internal static class CallbackExtension
    {
        public static string WriteNative(this Callback callback, Namespace currentNamespace)
        {
            var returnType = callback.ReturnValue.WriteNative(currentNamespace);
            var arguments = callback.Arguments.WriteNative(currentNamespace);
            return $"public delegate {returnType} {callback.SymbolName}({arguments});";
        }
    }
}
