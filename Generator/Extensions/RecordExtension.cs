using System.Linq;
using Repository;
using Repository.Model;

namespace Generator
{
    public static class RecordExtension
    {
        public static string WriteReleaseMemoryCall(this Record record)
        {
            //Unref functions takes precedense over free function
            if (record.Methods.Any(IsUnref))
                return "Native.Methods.Unref(this);";

            if(record.Methods.Any(IsFree))
                return "Native.Methods.Free(this);";

            return "//TODO: No method to release data found.";
        }

        private static bool IsUnref(Method method) => method.SymbolName == "Unref";
        private static bool IsFree(Method method) => method.SymbolName == "Free";
    }
}
