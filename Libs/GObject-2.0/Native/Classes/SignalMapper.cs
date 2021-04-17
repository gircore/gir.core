using System;
using System.Collections.Generic;
using GLib.Native;

namespace GObject.Native
{
    public class SignalMapper
    {
        private static readonly Dictionary<GObject.Object, Dictionary<string, GObject.Object.SignalHelper>> _signals = new();
        
        //TODO
    }
}
