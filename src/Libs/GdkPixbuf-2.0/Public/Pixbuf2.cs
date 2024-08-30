using System;
using System.Runtime.CompilerServices;
using GdkPixbuf.Internal;
using GObject;
using GObject.Internal;
using Type = GObject.Type;

namespace GdkPixbuf
{
    public class Pixbuf2 : GObject.Object2
    {
        internal Pixbuf2(Pixbuf2Handle handle) : base(handle) { }
    
        internal static Pixbuf2 New(IntPtr handle, bool ownsHandle)
        {
            var h = new Pixbuf2Handle(handle, ownsHandle);
            return new Pixbuf2(h);
        }
        
        public static Pixbuf2 New(Colorspace colorspace, bool hasAlpha, int bitsPerSample, int width, int height)
        {
            //TODO: How is the instance kept alive in case C# does not need it anymore, but C does?
            var handle = Internal.Pixbuf.New(colorspace, hasAlpha, bitsPerSample, width, height);
            return New(handle, true);
        }
    
    
        [Version("2.12")]
        public Pixbuf2? ApplyEmbeddedOrientation()
        {
            var resultApplyEmbeddedOrientation = GdkPixbuf.Internal.Pixbuf.ApplyEmbeddedOrientation(GetHandle());
            
            return ObjectWrapper2.WrapNullableHandle<Pixbuf2>(resultApplyEmbeddedOrientation, true);
        }
    }
}

namespace GdkPixbuf.Internal
{
    public class Pixbuf2Handle : GObject.Internal.Object2Handle
    {
        private long _size;
    
        internal Pixbuf2Handle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
        {
        }

        protected override void AddMemoryPressure()
        {
            _size = (long) Internal.Pixbuf.GetByteLength(handle);
            GC.AddMemoryPressure(_size);
        }

        protected override void RemoveMemoryPressure()
        {
            GC.RemoveMemoryPressure(_size);
        }
    }
}



