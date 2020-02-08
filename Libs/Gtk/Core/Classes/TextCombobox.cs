using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GTextCombobox : GCombobox
    {
        public GTextCombobox(string template, string obj = "combobox") : base(template, obj, Assembly.GetCallingAssembly()) {}
        public GTextCombobox() : this(Gtk.ComboBoxText.@new()){}
        internal GTextCombobox(IntPtr handle) : base(handle) { }

        public void AppendText(string id, string text) => Gtk.ComboBoxText.insert(this, -1, id, text);
        public void RemoveAll() => Gtk.ComboBoxText.remove_all(this);
    }
}