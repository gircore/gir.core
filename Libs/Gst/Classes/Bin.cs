using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GObject;

namespace Gst
{
    public partial class Bin
    {
        public Bin(string name) : this(ConstructParameter.With(NameProperty, name)) { }

        public bool Add(Element element) => Native.add(Handle, element.Handle);
        public bool Remove(Element element) => Native.remove(Handle, element.Handle);
        
        public IEnumerable<Element> IterateRecurse()
        {
            throw new NotImplementedException();
        }
    }
}
