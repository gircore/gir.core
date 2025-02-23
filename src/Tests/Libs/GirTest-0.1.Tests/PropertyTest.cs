using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PropertyTest : Test
{
    [DataTestMethod]
    [DataRow("NewTitle")]
    [DataRow("Some Text With Unicode ☀🌙🌧")]
    public void TestStringProperty(string str)
    {
        var obj = PropertyTester.New();
        obj.StringValue = str;

        obj.StringValue.Should().Be(str);
    }

    [TestMethod]
    public void TestNullStringProperty()
    {
        var text = "text";
        var obj = PropertyTester.New();
        obj.StringValue = text;
        obj.StringValue.Should().Be(text);
        obj.StringValue = null;
        obj.StringValue.Should().BeNull();

        PropertyTester.StringValuePropertyDefinition.UnmanagedName.Should().Be("string-value");
        PropertyTester.StringValuePropertyDefinition.ManagedName.Should().Be(nameof(PropertyTester.StringValue));
    }

    [TestMethod]
    public void PropertiesCanBeUsedToReceiveNotifyCallbacks()
    {
        var value = "MyNewValue";
        var result = string.Empty;

        var obj = PropertyTester.New();
        PropertyTester.StringValuePropertyDefinition.Notify(obj, StringValueSignalHandler);

        obj.StringValue = value;
        result.Should().Be(value);

        PropertyTester.StringValuePropertyDefinition.Unnotify(obj, StringValueSignalHandler);

        void StringValueSignalHandler(GObject.Object sender, GObject.Object.NotifySignalArgs args)
        {
            result = ((PropertyTester) sender).StringValue;
        }
    }

    [TestMethod]
    public void PropertyNamedLikeClass()
    {
        //Properties named like a class are suffixed with an underscore.
        var obj = PropertyTester.New();
        obj.PropertyTester_ = "test";

        PropertyTester.PropertyTester_PropertyDefinition.UnmanagedName.Should().Be("property-tester");
        PropertyTester.PropertyTester_PropertyDefinition.ManagedName.Should().Be(nameof(PropertyTester.PropertyTester_));
    }

    [TestMethod]
    public void SupportsTypedRecordProperty()
    {
        var r = TypedRecordTester.New();
        r.CustomBitfield = TypedRecordTesterBitfield.One | TypedRecordTesterBitfield.Zero;
        var obj = PropertyTester.New();
        obj.RecordValue = r;

        obj.RecordValue.CustomBitfield.Should().Be(TypedRecordTesterBitfield.One | TypedRecordTesterBitfield.Zero);

        PropertyTester.RecordValuePropertyDefinition.UnmanagedName.Should().Be("record-value");
        PropertyTester.RecordValuePropertyDefinition.ManagedName.Should().Be(nameof(PropertyTester.RecordValue));
    }

    [DataTestMethod]
    [DataRow(10)]
    [DataRow(-10)]
    [DataRow(0)]
    public void TestIntProperty(int i)
    {
        var obj = PropertyTester.New();
        obj.IntValue = i;

        obj.IntValue.Should().Be(i);
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void TestBooleanProperty(bool b)
    {
        var obj = PropertyTester.New();
        obj.BooleanValue = b;

        obj.BooleanValue.Should().Be(b);
    }

    [TestMethod]
    public void TestObjectProperty()
    {
        var o = new PropertyTester();

        var obj = PropertyTester.New();
        obj.ObjectValue.Should().BeNull();
        obj.ObjectValue = o;
        obj.ObjectValue.Should().Be(o);
        obj.ObjectValue = null;
        obj.ObjectValue.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReadOnlyProperty()
    {
        var property = typeof(PropertyTester).GetProperty(nameof(PropertyTester.ExecutorValue));
        property.Should().BeReadable();
        property.Should().NotBeWritable();
    }

    [TestMethod]
    public void PrefersKnownTypeForInterfaceReturnInsteadOfHelper()
    {
        var property = typeof(PropertyTester).GetProperty(nameof(PropertyTester.ExecutorValue));
        property.Should().Return(typeof(Executor));

        var obj = PropertyTester.New();
        obj.ExecutorValue.Should().BeOfType<ExecutorImpl>();
    }

    [TestMethod]
    public void FallsBackToInterfaceHelperForUnknownTypesWhichImplementAnInterface()
    {
        var property = typeof(PropertyTester).GetProperty(nameof(PropertyTester.ExecutorAnonymousValue));
        property.Should().Return(typeof(Executor));

        var obj = PropertyTester.New();
        obj.ExecutorAnonymousValue.Should().BeOfType<ExecutorHelper>();
    }
}
