using System.CodeDom.Compiler;
using System.Linq;
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
    public void ShoudHaveAGtype()
    {
        SomeSubClass.GetGType().Value.Should().NotBe(0);
    }

    [TestMethod]
    public void GenericSubclassesShouldBePossible()
    {
        var type1 = SomeGenericSubclass<int>.GetGType();
        var type2 = SomeGenericSubclass2<int, int>.GetGType();
        var type3 = SomeGenericSubclass2<SomeSubClass, string>.GetGType();
        var type4 = SomeGenericSubclass<SomeGenericSubclass<string>>.GetGType();
        var type5 = SomeSubSubClass.GetGType();
        var type6 = SomeClassContainingNestedSubSubSubClass.SomeNestedSubSubSubClass.GetGType();
        var type7 = SomeGlobalSubClass.GetGType();

        type1.Should().NotBe(type2);
        type1.Should().NotBe(type3);
        type1.Should().NotBe(type4);
        type1.Should().NotBe(type5);
        type1.Should().NotBe(type6);
        type1.Should().NotBe(type7);

        type2.Should().NotBe(type3);
        type2.Should().NotBe(type4);
        type2.Should().NotBe(type5);
        type2.Should().NotBe(type6);
        type2.Should().NotBe(type7);

        type3.Should().NotBe(type4);
        type3.Should().NotBe(type5);
        type3.Should().NotBe(type6);
        type3.Should().NotBe(type7);

        type4.Should().NotBe(type5);
        type4.Should().NotBe(type6);
        type4.Should().NotBe(type7);

        type5.Should().NotBe(type6);
        type5.Should().NotBe(type7);

        type6.Should().NotBe(type7);
    }
}
