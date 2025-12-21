using System;

namespace Gtk;

[AttributeUsage(AttributeTargets.Class)]
public class TemplateAttribute(string ressourceName) : Attribute
{
    public string RessourceName { get; } = ressourceName;
}

public interface TemplateLoader
{
    static abstract GLib.Bytes Load(string resourceName);
}