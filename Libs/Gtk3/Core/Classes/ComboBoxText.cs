using System;
using System.Reflection;

namespace Gtk
{
    public partial class ComboBoxText
    {
        public ComboBoxText(string template, string obj = "combobox") : base(template, obj, Assembly.GetCallingAssembly()) {}
        public ComboBoxText() : this(Sys.ComboBoxText.@new()){}

        public void AppendText(string id, string text) => Sys.ComboBoxText.insert(Handle, -1, id, text);
        public void RemoveAll() => Sys.ComboBoxText.remove_all(Handle);
    }
}