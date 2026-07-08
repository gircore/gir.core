using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class SubclassTests : Test
{
    [TestMethod]
    public void ShouldNotCreateObsoleteInstances()
    {
        var obj = MySubSubclass.NewWithProperties([]);
        obj.GetCounter().Should().Be(2);
    }
}

[GObject.Subclass<GirTest.SubClassTester>]
public partial class MySubclass
{
    partial void Initialize()
    {
        Init();
    }

    protected virtual void Init()
    {
        IncreaseCounter();
    }
}

[GObject.Subclass<MySubclass>]
public partial class MySubSubclass
{
    protected override void Init()
    {
        IncreaseCounter();

        base.Init();
    }
}
