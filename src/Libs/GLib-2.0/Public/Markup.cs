using System;

namespace GLib;

public static class Markup
{
    public static string EscapeText(string text)
    {
        return Internal.Functions.MarkupEscapeText(Internal.NonNullableUtf8StringOwnedHandle.Create(text), -1).ConvertToString()
            ?? throw new Exception("Non nullable return type returned a null");
    }
}
