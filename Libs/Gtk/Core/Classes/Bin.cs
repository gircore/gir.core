using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GBin : GContainer
    {
        internal GBin(string template, string obj, Assembly assembly) : base(template, obj, assembly){}
        internal protected GBin(IntPtr handle) : base(handle) { }
    }
}