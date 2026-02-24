using System;

namespace Gtk;

[AttributeUsage(AttributeTargets.Class)]
public class TemplateAttribute<TLoader>(string resourceName) : Attribute where TLoader : TemplateLoader
{
    public string ResourceName { get; } = resourceName;
}
