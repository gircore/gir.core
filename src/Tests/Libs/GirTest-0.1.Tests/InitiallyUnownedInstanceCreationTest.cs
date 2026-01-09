using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public partial class InitiallyUnownedInstanceCreationTest : Test
{
    [TestMethod]
    public void NewWithPropertiesShouldHaveCorrectRefCount()
    {
        var obj = InitiallyUnownedInstanceCreationTester.NewWithProperties([]);

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void NewShouldHaveCorrectRefCount()
    {
        var obj = InitiallyUnownedInstanceCreationTester.New();

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SetInstanceTransferFullShouldHaveCorrectRefCount()
    {
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = InitiallyUnownedInstanceCreationTester.New();

        obj1.SetObjTransferFull(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SetInstanceTransferNoneShouldHaveCorrectRefCount()
    {
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = InitiallyUnownedInstanceCreationTester.New();

        obj1.SetObjTransferNone(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void ReceiveTransferFull()
    {
        var obj = (InitiallyUnownedInstanceCreationTester) InitiallyUnownedInstanceCreationTester.CreateTransferFull(InitiallyUnownedInstanceCreationTester.GetGType());

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void ReceiveTransferNone()
    {
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = (InitiallyUnownedInstanceCreationTester) obj1.CreateTransferNone(InitiallyUnownedInstanceCreationTester.GetGType());

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
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = MySubclass.NewWithProperties([]);

        obj1.SetObjTransferFull(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SetSubclassInstanceTransferNoneShouldHaveCorrectRefCount()
    {
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = MySubclass.NewWithProperties([]);

        obj1.SetObjTransferNone(obj2);

        obj2.GetRefCount().Should().Be(2); //dotnet ref + obj1 ref
        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1); //dotnet ref
    }

    [TestMethod]
    public void SubclassReceiveTransferFull()
    {
        var obj = (MySubclass) InitiallyUnownedInstanceCreationTester.CreateTransferFull(MySubclass.GetGType());

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [TestMethod]
    public void SubclassReceiveTransferNone()
    {
        var obj1 = InitiallyUnownedInstanceCreationTester.New();
        var obj2 = (MySubclass) obj1.CreateTransferNone(MySubclass.GetGType());

        obj2.IsFloating().Should().BeFalse();
        obj2.GetRefCount().Should().Be(2);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(2);

        obj1.Dispose();
        obj2.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [GObject.Subclass<InitiallyUnownedInstanceCreationTester>]
    private partial class MySubclass
    {

    }
}
