namespace GLib
{
    public static class Markup
    {
        public static string EscapeText(string text)
        {
            return Native.Functions.MarkupEscapeText(text, -1);
        }
    }
}
