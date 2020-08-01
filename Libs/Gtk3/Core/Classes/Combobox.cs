using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public class Combobox : Bin
    {
        private readonly Property<string> activeId;
        public Property<string> ActiveId => activeId;

        internal Combobox(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties(out activeId);
        }
        internal Combobox(IntPtr handle) : base(handle) 
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