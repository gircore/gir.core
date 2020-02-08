using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public class GCombobox : GBin
    {
        public Property<string> ActiveId { get; private set; } = default!;

        internal GCombobox(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties();
        }
        internal GCombobox(IntPtr handle) : base(handle) 
        { 
            InitProperties();
        }

        private void InitProperties()
        {
            ActiveId = Property<string>("active-id",
                get : GetStr,
                set: Set
            );
        }
    }
}