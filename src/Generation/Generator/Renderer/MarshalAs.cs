namespace Generator.Renderer;

internal static class MarshalAs
{
    public static string UnmanagedLpArray(int sizeParamIndex)
    {
        return $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={sizeParamIndex})]";
    }
}
