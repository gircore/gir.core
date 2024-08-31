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
    public class MyPixbuf : Pixbuf2, RegisteredGType
    {
        private static readonly Type gType;
        static MyPixbuf()
        {
            gType = TypeRegistrar2.Register<MyPixbuf>(Pixbuf2.GetGType());
        }

        public MyPixbuf() : base(gType, true, Array.Empty<ConstructArgument>())
        {
        }

        protected MyPixbuf(IntPtr handle, bool ownsHandle) : base(new Pixbuf2Handle(handle, ownsHandle)) { }

        public new static Type GetGType() => gType;

        public new static Object2 Create(IntPtr handle, bool ownsHandle)
        {
            return new MyPixbuf(handle, ownsHandle);
        }
    }
    public class Pixbuf2 : GObject.Object2, RegisteredGType
    {
        protected internal Pixbuf2(Pixbuf2Handle handle) : base(handle) { }
        protected Pixbuf2(Type type, bool owned, ConstructArgument[] constructArguments) : base(type, owned, constructArguments) { }
        
        public static Pixbuf2 New(Colorspace colorspace, bool hasAlpha, int bitsPerSample, int width, int height)
        {
            var handle = Internal.Pixbuf.New(colorspace, hasAlpha, bitsPerSample, width, height);
            return (Pixbuf2) Create(handle, true);
        }
        
        public static Type GetGType()
        {
            var resultGetGType = GdkPixbuf.Internal.Pixbuf.GetGType();
            return resultGetGType;
        }

        public static Object2 Create(IntPtr handle, bool ownsHandle)
        {
            var safeHandle = new Pixbuf2Handle(handle, ownsHandle);
            return new Pixbuf2(safeHandle);
        }


        [Version("2.12")]
        public Pixbuf2? ApplyEmbeddedOrientation()
        {
            var resultApplyEmbeddedOrientation = GdkPixbuf.Internal.Pixbuf.ApplyEmbeddedOrientation(GetHandle());
            
            return InstanceWrapper.WrapNullableHandle<Pixbuf2>(resultApplyEmbeddedOrientation, true);
        }
    }
}

namespace GdkPixbuf.Internal
{
    internal class TypeRegistration2
    {
        internal static void RegisterTypes()
        {
            Register<Pixbuf2>(OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows);
        }

        private static void Register<T>(params OSPlatform[] supportedPlatforms) where T : RegisteredGType
        {
            try
            {
                if(supportedPlatforms.Any(RuntimeInformation.IsOSPlatform))
                    GObject.Internal.InstanceFactory.Register(new Type(T.GetGType()), T.Create);
            }
            catch(System.Exception e)
            {
                Debug.WriteLine($"Could not register type: {e.Message}");
            }
        }
    }
    
    public class Pixbuf2Handle : GObject.Internal.Object2Handle
    {
        private long _size;
    
        public Pixbuf2Handle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
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



