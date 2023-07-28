using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests.Classes;

internal class GenericClass : GObject.Object
{
    public GenericClass() : base(true, Array.Empty<GObject.ConstructArgument>())
    {
    }
}
internal class GenericClass<T> : GObject.Object
{
    public GenericClass() : base(true, Array.Empty<GObject.ConstructArgument>())
    {
    }
}
internal class GenericClass<T1, T2> : GObject.Object
{
    public GenericClass() : base(true, Array.Empty<GObject.ConstructArgument>())
    {
    }
}

[TestClass, TestCategory("UnitTest")]
public class GenericTypeRegistrationTests
{
    private class NestedGenericClass<T> : GObject.Object
    {
        public NestedGenericClass() : base(true, Array.Empty<GObject.ConstructArgument>())
        {
        }
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
