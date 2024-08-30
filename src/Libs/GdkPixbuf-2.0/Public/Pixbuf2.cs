using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GdkPixbuf.Internal;
using GObject;
using GObject.Internal;
using Type = GObject.Type;

namespace GdkPixbuf
{
    public class Pixbuf2 : GObject.Object2
    {
        internal Pixbuf2(Pixbuf2Handle handle) : base(handle) { }
        
        public static Pixbuf2 New(Colorspace colorspace, bool hasAlpha, int bitsPerSample, int width, int height)
        {
            var handle = Internal.Pixbuf.New(colorspace, hasAlpha, bitsPerSample, width, height);
            return Pixbuf2InstanceFactory.Create(handle, true);
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
    internal class Pixbuf2InstanceFactory
    {
        public static Pixbuf2 Create(IntPtr handle, bool ownsHandle)
        {
            var h = new Pixbuf2Handle(handle, ownsHandle);
            return new Pixbuf2(h);
        }
    }
    internal class TypeRegistration2
    {
        internal static void RegisterTypes()
        {
            Register(Pixbuf.GetGType, Pixbuf2InstanceFactory.Create, OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows);

        
        }

        private static void Register(Func<nuint> getType, InstanceFactoryForType factory, params OSPlatform[] supportedPlatforms)
        {
            try
            {
                if(supportedPlatforms.Any(RuntimeInformation.IsOSPlatform))
                    GObject.Internal.InstanceFactory.Register(new Type(getType()), factory);
            }
            catch(System.Exception e)
            {
                Debug.WriteLine($"Could not register type '{nameof(T)}': {e.Message}");
            }
        }
    }
    
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



