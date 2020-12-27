using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GObject;

namespace Gst
{
    public partial struct Iterator : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new IteratorEnum(this);
        }
        
        private class IteratorEnum : IEnumerator
        {
            private object? _current;
            
            private Iterator _iter;
            
            public IteratorEnum(Iterator iter)
            {
                _iter = iter;
            }

            object IEnumerator.Current { get => _current ?? throw new IndexOutOfRangeException(); }

            public bool MoveNext()
            {
                var result = _iter.Next(out Value value);

                switch (result)
                {
                    case IteratorResult.Ok:
                        _current = value.Extract();
                        value.Dispose();
                        break;
                    
                    case IteratorResult.Resync:
                        throw new NotImplementedException("Resync is not yet implemented for GstIterator. We do not currently support GStreamer in a multithreaded context");

                    case IteratorResult.Error:
                        Console.WriteLine("Gst.Iterator suffered an internal error");
                        return false;

                    case IteratorResult.Done:
                        return false;
                }

                return true;
            }

            public void Reset()
            {
                throw new System.NotImplementedException("Reset is not yet implemented for GstIterator");
            }
        }

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
    }
}
