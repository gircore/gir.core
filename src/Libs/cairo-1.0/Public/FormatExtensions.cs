namespace Cairo;

public static class FormatExtensions
{
    public static int StrideForWidth(this Format format, int width)
    {
        return Internal.Format.StrideForWidth(format, width);
    }
}
