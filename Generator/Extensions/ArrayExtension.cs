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
            
            // TODO: Since we are using SizeParamIndex, let's handle this within method generation
            // so the user does not have to provide the array length, only the managed array.

            return attribute;
        }
    }
}
