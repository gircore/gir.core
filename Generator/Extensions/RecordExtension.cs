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
            if (record.Methods.Any(x => x.IsUnref()))
                return "Native.Methods.Unref(handle);";

            if(record.Methods.Any(x => x.IsFree()))
                return "Native.Methods.Free(handle);";

            return "//TODO: No method to release data found.";
        }
    }
}
