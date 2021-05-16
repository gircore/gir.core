using System;
using System.Linq;
using Gir;
using Gir.Output.Model;

namespace Generator
{
    public static class RecordExtension
    {
        public static string WriteReleaseMemoryCall(this Record record)
        {
            var name = record.Metadata["Name"]?.ToString() ?? throw new Exception("Record is missing its name");

            //Unref functions takes precedense over free function
            if (record.Methods.Any(x => x.IsUnref()))
                return $"Native.{name}.Methods.Unref(handle);";

            if (record.Methods.Any(x => x.IsFree()))
                return $"Native.{name}.Methods.Free(handle);";

            return "//TODO: No method to release data found.";
        }
    }
}
