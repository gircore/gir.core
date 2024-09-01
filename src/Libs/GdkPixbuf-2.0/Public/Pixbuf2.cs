﻿using System;
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
    public interface TestInterface
    {
        void Bla();
    }

    public class TestInterfaceHelper : Object2, TestInterface, InterfaceFactory, GTypeProvider
    {
        public TestInterfaceHelper(Object2Handle handle) : base(handle)
        {
        }

        public void Bla()
        {
        }

        static Object2 InterfaceFactory.Create(IntPtr handle, bool ownsHandle)
        {
            return new TestInterfaceHelper(new Object2Handle(handle, ownsHandle));
        }

        static Type GTypeProvider.GetGType()
        {
            throw new NotImplementedException();
        }
    }
    
    public class MyPixbuf : Pixbuf2, ClassFactory, GTypeProvider
    {
        private static readonly Type GType;
        static MyPixbuf()
        {
            GType = SubclassRegistrar2.Register<MyPixbuf, Pixbuf2>();
        }

        public MyPixbuf() : base(Pixbuf2Handle.For<MyPixbuf>(true, []))
        {
        }

        private MyPixbuf(IntPtr handle, bool ownsHandle) : base(new Pixbuf2Handle(handle, ownsHandle)) { }

        static Type GTypeProvider.GetGType() => GType;

        static Object2 ClassFactory.Create(IntPtr handle, bool ownsHandle)
        {
            return new MyPixbuf(handle, ownsHandle);
        }
    }
    public class Pixbuf2 : GObject.Object2, ClassFactory, GTypeProvider
    {
        protected internal Pixbuf2(Pixbuf2Handle handle) : base(handle) { }
        
        public static Pixbuf2 New(Colorspace colorspace, bool hasAlpha, int bitsPerSample, int width, int height)
        {
            var handle = Internal.Pixbuf.New(colorspace, hasAlpha, bitsPerSample, width, height);
            return (Pixbuf2) Create(handle, true);
        }
        
        private static Object2 Create(IntPtr handle, bool ownsHandle)
        {
            var safeHandle = new Pixbuf2Handle(handle, ownsHandle);
            return new Pixbuf2(safeHandle);
        }
        
        static Type GTypeProvider.GetGType()
        {
            var resultGetGType = GdkPixbuf.Internal.Pixbuf.GetGType();
            return resultGetGType;
        }

        static Object2 ClassFactory.Create(IntPtr handle, bool ownsHandle)
        {
            return Create(handle, ownsHandle);
        }


        [Version("2.12")]
        public Pixbuf2? ApplyEmbeddedOrientation()
        {
            var resultApplyEmbeddedOrientation = GdkPixbuf.Internal.Pixbuf.ApplyEmbeddedOrientation(GetHandle());
            
            return (Pixbuf2?) InstanceWrapper.WrapNullableHandle(resultApplyEmbeddedOrientation, true);
        }
    }
}

namespace GdkPixbuf.Internal
{
    internal static class TypeRegistration2
    {
        public static void RegisterTypes()
        {
            Register<Pixbuf2>(OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows);
        }

        private static void Register<T>(params OSPlatform[] supportedPlatforms) where T : ClassFactory, GTypeProvider
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
        
        public static Pixbuf2Handle For<T>(bool owned, ConstructArgument[] constructArguments) where T : Pixbuf2, GTypeProvider
        {
            // We can't check if a reference is floating via "g_object_is_floating" here
            // as the function could be "lying" depending on the intent of framework writers.
            // E.g. A Gtk.Window created via "g_object_new_with_properties" returns an unowned
            // reference which is not marked as floating as the gtk toolkit "owns" it.
            // For this reason we just delegate the problem to the caller and require a
            // definition whether the ownership of the new object will be transferred to us or not.

            var ptr = GObject.Internal.Object.NewWithProperties(
                objectType: T.GetGType(),
                nProperties: (uint) constructArguments.Length,
                names: constructArguments.Select(x => x.Name).ToArray(),
                values: ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
            );
            
            return new Pixbuf2Handle(ptr, owned);
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



