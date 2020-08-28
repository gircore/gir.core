using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class ComboBox
    {
        private readonly IProperty<string> activeId;
        public IProperty<string> ActiveId => activeId;

        internal ComboBox(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties(out activeId);
        }

        //Workaround for: https://github.com/dotnet/roslyn/issues/32358
        private void InitProperties(out IProperty<string> activeId)
        {
            activeId = PropertyOfString("active-id");
        }
    }
}