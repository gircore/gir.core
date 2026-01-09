using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public partial class InstanceCreationTest : Test
{
    [TestMethod]
    public void NewWithPropertiesShouldHaveCorrectRefCount()
    {
        var obj = InstanceCreationTester.NewWithProperties([]);

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void NewShouldHaveCorrectRefCount()
    {
        var obj = InstanceCreationTester.New();

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SetInstanceTransferFullShouldHaveCorrectRefCount()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = InstanceCreationTester.New();

        obj1.SetObjTransferFull(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SetInstanceTransferNoneShouldHaveCorrectRefCount()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = InstanceCreationTester.New();

        obj1.SetObjTransferNone(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void ReceiveTransferFull()
    {
        var obj = (InstanceCreationTester) InstanceCreationTester.CreateTransferFull(InstanceCreationTester.GetGType());

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void ReceiveTransferNone()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = (InstanceCreationTester) obj1.CreateTransferNone(InstanceCreationTester.GetGType());

        obj2.IsFloating().Should().BeFalse();
        obj2.GetRefCount().Should().Be(2);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(2);

        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }


    [TestMethod]
    public void InterfaceReceiveTransferFull()
    {
        var obj = (ExecutorHelper) InstanceCreationTester.InterfaceCreateTransferFull();

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void InterfaceReceiveTransferNone()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = (ExecutorHelper) obj1.InterfaceCreateTransferNone();

        obj2.IsFloating().Should().BeFalse();
        obj2.GetRefCount().Should().Be(2);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(2);

        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SubclassNewWithPropertiesShouldHaveCorrectRefCount()
    {
        var obj = MySubclass.NewWithProperties([]);

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SetSubclassInstanceTransferFullShouldHaveCorrectRefCount()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = MySubclass.NewWithProperties([]);

        obj1.SetObjTransferFull(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SetSubclassInstanceTransferNoneShouldHaveCorrectRefCount()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = MySubclass.NewWithProperties([]);

        obj1.SetObjTransferNone(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SubclassReceiveTransferFull()
    {
        var obj = (MySubclass) InstanceCreationTester.CreateTransferFull(MySubclass.GetGType());

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SubclassReceiveTransferNone()
    {
        var obj1 = InstanceCreationTester.New();
        var obj2 = (MySubclass) obj1.CreateTransferNone(MySubclass.GetGType());

        obj2.IsFloating().Should().BeFalse();
        obj2.GetRefCount().Should().Be(2);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(2);

        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [GObject.Subclass<InstanceCreationTester>]
    private partial class MySubclass;

    [TestMethod]
    public void LegacyNewConstructorShouldHaveCorrectRefCount()
    {
        var obj = new InstanceCreationTester();

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    private class MyLegacySubclass : InstanceCreationTester;

    [TestMethod]
    public void LegacySubclass()
    {
        var obj = new MyLegacySubclass();

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }
}
