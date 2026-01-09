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
    public void SubclassNewWithPropertiesShouldHaveCorrectRefCount()
    {
        var obj = MySubclass.NewWithProperties([]);

        obj.IsFloating().Should().BeFalse();
        obj.GetRefCount().Should().Be(1);
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
    }

    [GObject.Subclass<InstanceCreationTester>]
    private partial class MySubclass
    {
        
    }
}
