namespace Generator3.Model
{
    public static class MarshalAs
    {
        public static string UnmanagedLpArray(int sizeParamIndex)
        {
            return $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={sizeParamIndex})]";
        }

        public static string UnmanagedLpString()
        {
            return "[MarshalAs(UnmanagedType.LPStr)]";
        }

        public static string UnmanagedLpUtf8String()
        {
            return "[MarshalAs(UnmanagedType.LPUTF8Str)]";
        }

        public static string UnmanagedByValArray(int sizeConst)
        {
            return $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {sizeConst})]";
        }
    }
}
