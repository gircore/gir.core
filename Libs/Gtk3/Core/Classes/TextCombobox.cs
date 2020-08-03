using System;
using System.Reflection;

namespace Gtk
{
    public class TextCombobox : Combobox
    {
        public TextCombobox(string template, string obj = "combobox") : base(template, obj, Assembly.GetCallingAssembly()) {}
        public TextCombobox() : this(Sys.ComboBoxText.@new()){}
        internal TextCombobox(IntPtr handle) : base(handle) { }

        public void AppendText(string id, string text) => Sys.ComboBoxText.insert(this, -1, id, text);
        public void RemoveAll() => Sys.ComboBoxText.remove_all(this);
    }
}