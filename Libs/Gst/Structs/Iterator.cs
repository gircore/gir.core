using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GObject;

namespace Gst
{
    // TODO: Implement IEnumerator for this class
    public partial struct Iterator
    {
        public IteratorResult Next(out Value elem)
        {
            // TODO: Fix generator to properly marshal as ref struct
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, ptr, false);

            elem = new Value();
            
            var result = Native.next(ptr, ref elem);
            
            Marshal.PtrToStructure(ptr, this);
            Marshal.FreeHGlobal(ptr);
            
            return result;
        }

        // TODO: We need to unset Value at some point
        // Is this is a memory leak?
        public IEnumerable<Value> GetValues()
        {
            bool done = false;
            while (!done)
            {
                Value elem;
                switch (Next(out elem))
                {
                    case IteratorResult.Ok:
                        yield return elem;
                        break;
                    
                    case IteratorResult.Resync:
                        // Do something here
                        break;
                    
                    case IteratorResult.Error:
                        done = true;
                        break;
                        
                    case IteratorResult.Done:
                        done = true;
                        break;
                }
            }
        }
    }
}
