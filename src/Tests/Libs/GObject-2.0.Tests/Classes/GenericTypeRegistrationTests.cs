using System;
using GObject.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests.Classes;

internal class GenericClass : GObject.Object, GTypeProvider, InstanceFactory
{
    private static readonly Type GType = SubclassRegistrar.Register<GenericClass, Object>();
    public static new Type GetGType() => GType;
    static object InstanceFactory.Create(IntPtr handle, bool ownsHandle)
    {
        return new GenericClass(handle, ownsHandle);
    }

    public GenericClass() : base(ObjectHandle.For<GenericClass>(true, [])) { }
    private GenericClass(IntPtr ptr, bool ownsHandle) : base(new ObjectHandle(ptr, ownsHandle)) { }
}
internal class GenericClass<T> : GObject.Object, GTypeProvider, InstanceFactory
{
    private static readonly Type GType = SubclassRegistrar.Register<GenericClass<T>, Object>();
    public static new Type GetGType() => GType;
    static object InstanceFactory.Create(IntPtr handle, bool ownsHandle)
    {
        return new GenericClass<T>(handle, ownsHandle);
    }

    public GenericClass() : base(ObjectHandle.For<GenericClass<T>>(true, [])) { }
    private GenericClass(IntPtr ptr, bool ownsHandle) : base(new ObjectHandle(ptr, ownsHandle)) { }
}
internal class GenericClass<T1, T2> : GObject.Object, GTypeProvider, InstanceFactory
{
    private static readonly Type GType = SubclassRegistrar.Register<GenericClass<T1, T2>, Object>();
    public static new Type GetGType() => GType;
    static object InstanceFactory.Create(IntPtr handle, bool ownsHandle)
    {
        return new GenericClass<T1, T2>(handle, ownsHandle);
    }

    public GenericClass() : base(ObjectHandle.For<GenericClass<T1, T2>>(true, [])) { }
    private GenericClass(IntPtr ptr, bool ownsHandle) : base(new ObjectHandle(ptr, ownsHandle)) { }
}

[TestClass, TestCategory("UnitTest")]
public class GenericTypeRegistrationTests
{
    private class NestedGenericClass<T> : GObject.Object, GTypeProvider, InstanceFactory
    {
        private static readonly Type GType = SubclassRegistrar.Register<NestedGenericClass<T>, Object>();
        public static new Type GetGType() => GType;
        static object InstanceFactory.Create(IntPtr handle, bool ownsHandle)
        {
            return new NestedGenericClass<T>(handle, ownsHandle);
        }

        public NestedGenericClass() : base(ObjectHandle.For<NestedGenericClass<T>>(true, [])) { }
        private NestedGenericClass(IntPtr ptr, bool ownsHandle) : base(new ObjectHandle(ptr, ownsHandle)) { }
    }

    [TestMethod]
    public void TestDynamicGenericTypeRegistration()
    {
        // (Invalid names will cause the test host to crash.)
        using (new GenericClass<int, int>()) { };
        using (new GenericClass<GenericClass, string>()) { };
        using (new GenericClass<GenericClass<string>>()) { };
        using (new NestedGenericClass<GenericClass<string>>()) { };
    }
}
