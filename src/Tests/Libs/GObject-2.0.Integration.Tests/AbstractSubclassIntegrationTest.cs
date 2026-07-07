using AwesomeAssertions;
using DiagnosticAnalyzerTestProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class AbstractSubclassIntegrationTest : Test
{
    [TestMethod]
    public void AbstractSubclassShouldRegisterGType()
    {
        SomeAbstractSubClass.GetGType().Value.Should().NotBe(0);
    }

    [TestMethod]
    public void ConcreteSubclassOfAbstractSubclassShouldCallInitializeOnce()
    {
        var obj = SomeConcreteSubClass.NewWithProperties([]);

        obj.InitializedCount.Should().Be(1);
    }
}
