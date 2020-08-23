using System;
using System.Reflection;

namespace Gtk
{
    public partial class Bin
    {
        internal Bin(string template, string obj, Assembly assembly) : base(template, obj, assembly){}
        internal protected Bin(IntPtr handle) : base(handle) { }
    }
}