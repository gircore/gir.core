namespace Gtk
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TemplateAttribute : Attribute
    {
        public string Ui { get; }

        public TemplateAttribute(string ui)
        {
            Ui = ui;
        }
    }
}
