namespace GLib
{
    public static class Markup
    {
        public static string EscapeText(string text)
        {
            return Functions.Native.MarkupEscapeText(text, -1);
        }
    }
}
