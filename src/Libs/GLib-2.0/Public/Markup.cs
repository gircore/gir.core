namespace GLib;

public static class Markup
{
    public static string EscapeText(string text)
    {
        return Internal.Functions.MarkupEscapeText(text, -1);
    }
}
