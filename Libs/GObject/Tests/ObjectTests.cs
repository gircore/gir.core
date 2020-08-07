using System;
using GObject;
using GObject.Sys;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GObject.Test
{
    public static class ObjectTests
    {
        class Subclass : GObject.Object
        {
            public Subclass() {}
        }

        public static void TestSimple()
        {
            var obj = new GObject.Object();
            var stringPtr = Sys.Methods.type_name(TypeDictionary.gtypeof(obj.GetType()));
            string? name = Marshal.PtrToStringAnsi(stringPtr);
            Debug.Assert("GObject" == name);

            Console.WriteLine(nameof(TestSimple) + " Passed!\n");
        }

        public static void TestSubclass()
        {
            var obj = new Subclass();
            IntPtr ptr = Sys.Methods.type_name_from_instance(obj.Handle);
            string? name = Marshal.PtrToStringAnsi(ptr);
            Console.WriteLine("Using custom GObject-integrated type: " + name);
            Debug.Assert("GObjectTestSubclass" == name);

            Console.WriteLine(nameof(TestSubclass) + " Passed!\n");
        }

        /*[Fact]
        public void TestWrapperLifecycle()
        {
            var obj = new GObject.Object();
            IntPtr reference = Sys.Object.@ref((IntPtr)obj);
            obj.Dispose();

        }

        [Fact]
        public void TestSubclassLifecycle()
        {
            // Create subclass
            var subclass = new Subclass();
        }*/
    }
}
