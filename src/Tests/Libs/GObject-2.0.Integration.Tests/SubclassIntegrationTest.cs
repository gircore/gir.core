using AwesomeAssertions;
using DiagnosticAnalyzerTestProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class SubclassIntegrationTest : Test
{
    [TestMethod]
    public void ShouldBeDerivedFromGObjectObject()
    {
        typeof(SomeSubClass).Should().BeDerivedFrom<GObject.Object>();
    }

    [TestMethod]
    public void ShouldImplementGObjectInstanceFactory()
    {
        typeof(SomeSubClass).Should().Implement(typeof(GObject.InstanceFactory));
    }

    [TestMethod]
    public void ShouldImplementGObjectGTypeProvider()
    {
        typeof(SomeSubClass).Should().Implement(typeof(GObject.GTypeProvider));
    }

    [TestMethod]
    public void ShouldHaveConstructArgumentConstructor()
    {
        typeof(SomeSubClass).Should().HaveConstructor([typeof(GObject.ConstructArgument[])]);
    }

    [TestMethod]
    public void PartialInitializeMethodShouldBeCalled()
    {
        var obj = new SomeInitializedSubClass();
        obj.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void ShouldHaveGtype()
    {
        SomeSubClass.GetGType().Value.Should().NotBe(0);
    }

    [TestMethod]
    public void ShouldAllowNestedTypes()
    {
        var type1 = SomeSubSubClass.GetGType();
        var type2 = SomeClassContainingNestedSubSubSubClass.SomeNestedSubSubSubClass.GetGType();
        var type3 = SomeGlobalSubClass.GetGType();

        type1.Should().NotBe(type2);
        type1.Should().NotBe(type3);

        type2.Should().NotBe(type3);
    }

    [TestMethod]    
    public void ShouldCallInitializeOnce()
    {
        // Test for https://github.com/gircore/gir.core/issues/1421
        const bool SomeRandomArgument = true;
        
        var obj = new SomeInitializedSubClass();
        var objWithUserConstructor = new SomeInitializedSubClassWithUserConstructor(SomeRandomArgument);
        
        obj.InitializedCount.Should().Be(1);
        objWithUserConstructor.InitializedCount.Should().Be(1);
    }
}
