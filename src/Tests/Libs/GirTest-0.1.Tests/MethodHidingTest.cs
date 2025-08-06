using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class MethodHidingTest : Test
{
    [TestMethod]
    public void CanCallNewToStringMethod()
    {
        var instance = MethodHidingSubclass.New();
        instance.ToString().Should().Be("New to_string");
    }

    [TestMethod]
    public void CanCallObjectToStringMethod()
    {
        var instance = MethodHidingSubclass.New();
        var asObj = (object) instance;
        asObj.ToString().Should().Contain("MethodHidingSubclass");
    }

    [TestMethod]
    public void CanCallNewMethodOnSubclass()
    {
        var instance = MethodHidingSubclass.New();
        instance.CustomString().Should().Be("Subclass custom string");
    }

    [TestMethod]
    public void CanCallNewMethodFromBaseClass()
    {
        var instance = MethodHidingSubclass.New();
        var asBase = ((MethodHidingBase) instance);
        asBase.CustomString().Should().Be("Base class custom string");
    }
}
