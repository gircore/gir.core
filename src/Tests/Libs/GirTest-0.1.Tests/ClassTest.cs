using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ClassTest : Test
{
    [TestMethod]
    public void CanDisposeInstanceAfterOwnershipIsTransferredAndUnrefed()
    {
        var obj = new TestClass();
        ClassTester.TransferOwnershipFullAndUnref(obj);
        var act = () => obj.Dispose();
        act.Should().NotThrow();
    }

    [TestMethod]
    public void InstanceIsGarbageCollectedAfterOwnershipTransferAndUnref()
    {
        var reference = new System.WeakReference(null);

        CollectAfter(() =>
        {
            var obj = new TestClass();
            reference.Target = obj;
            ClassTester.TransferOwnershipFullAndUnref(obj);
        });

        reference.IsAlive.Should().BeFalse();
    }

    private class TestClass : GObject.Object
    {
        public TestClass() : base(true, System.Array.Empty<GObject.ConstructArgument>()) { }
    }
}
