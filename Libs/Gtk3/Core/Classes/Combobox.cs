using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public class GCombobox : GBin
    {
        private Property<string> activeId;
        public Property<string> ActiveId => activeId;

        internal GCombobox(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties(out activeId);
        }
        internal GCombobox(IntPtr handle) : base(handle) 
        { 
            InitProperties(out activeId);
        }

        //Workaround for: https://github.com/dotnet/roslyn/issues/32358
        private void InitProperties(out Property<string> activeId)
        {
            activeId = PropertyOfString("active-id");
        }
    }
}