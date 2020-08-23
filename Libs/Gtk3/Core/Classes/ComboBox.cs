using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class ComboBox
    {
        private readonly Property<string> activeId;
        public Property<string> ActiveId => activeId;

        internal ComboBox(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties(out activeId);
        }
        internal ComboBox(IntPtr handle) : base(handle) 
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