using Repository.Model;

namespace Generator
{
    internal static class ArrayExtension
    {
        public static string GetMarshallAttribute(this Array? array, int offset)
        {               
            string attribute = "";
            if (array?.Length is { } length)
                attribute = $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={length + offset})]";
            
            // TODO: Can we use 'ArraySubType=UnmanagedType.LPUTF8Str'?
            // This might make this more robust

            return attribute;
        }
    }
}
