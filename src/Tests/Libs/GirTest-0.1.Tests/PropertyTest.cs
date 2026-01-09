using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PropertyTest : Test
{
    [TestMethod]
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

    [TestMethod]
    [DataRow(10)]
    [DataRow(-10)]
    [DataRow(0)]
    public void TestIntProperty(int i)
    {
        var obj = PropertyTester.New();
        obj.IntValue = i;

        obj.IntValue.Should().Be(i);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void TestBooleanProperty(bool b)
    {
        var obj = PropertyTester.New();
        obj.BooleanValue = b;

        obj.BooleanValue.Should().Be(b);
    }

    [TestMethod]
    [DataRow(42ul)]
    [DataRow(ulong.MinValue)]
    [DataRow(ulong.MaxValue)]
    public void TestUInt64Property(ulong val)
    {
        var obj = PropertyTester.New();
        obj.Uint64Value = val;

        obj.Uint64Value.Should().Be(val);
        obj.Uint64Value.Should().BeOfType(typeof(ulong));
    }

    [TestMethod]
    [DataRow(42)]
    [DataRow(long.MinValue)]
    [DataRow(long.MaxValue)]
    public void TestInt64Property(long val)
    {
        var obj = PropertyTester.New();
        obj.Int64Value = val;

        obj.Int64Value.Should().Be(val);
        obj.Int64Value.Should().BeOfType(typeof(long));
    }

    [TestMethod]
    [DataRow(42)]
    [DataRow(long.MinValue)]
    [DataRow(long.MaxValue)]
    [PlatformCondition(Platform.Unix64)]
    public void TestLongProperty64BitWideSystems(long val)
    {
        var obj = PropertyTester.New();
        obj.LongValue = val;

        obj.LongValue.Should().Be(val);
        obj.LongValue.Should().BeOfType(typeof(long));
    }

    [TestMethod]
    [DataRow(42)]
    [DataRow(int.MinValue)]
    [DataRow(int.MaxValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void TestLongProperty32BitWideSystems(int val)
    {
        var obj = PropertyTester.New();
        obj.LongValue = val;

        obj.LongValue.Should().Be(val);
        obj.LongValue.Should().BeOfType(typeof(long));
    }

    [TestMethod]
    [DataRow(42ul)]
    [DataRow(ulong.MinValue)]
    [DataRow(ulong.MaxValue)]
    [PlatformCondition(Platform.Unix64)]
    public void TestULongProperty64BitWideSystems(ulong val)
    {
        var obj = PropertyTester.New();
        obj.UlongValue = val;

        obj.UlongValue.Should().Be(val);
        obj.UlongValue.Should().BeOfType(typeof(ulong));
    }

    [TestMethod]
    [DataRow(42u)]
    [DataRow(uint.MinValue)]
    [DataRow(uint.MaxValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void TestULongProperty32BitWideSystems(uint val)
    {
        var obj = PropertyTester.New();
        obj.UlongValue = val;

        obj.UlongValue.Should().Be(val);
        obj.UlongValue.Should().BeOfType(typeof(ulong));
    }

    [TestMethod]
    public void TestObjectProperty()
    {
        var o = PropertyTester.New();

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
