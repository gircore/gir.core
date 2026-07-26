using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ListTest : Test
{
    [TestMethod]
    public void SupportsStringListReturnValueTransferFull()
    {
        //The strings are owned by the caller and are freed after they have been copied.
        ListTester.GetStringsTransferFull().Should().Equal("FOO", "BAR");
    }

    [TestMethod]
    public void SupportsStringListReturnValueTransferNone()
    {
        ListTester.GetStringsTransferNone().Should().Equal("FOO", "BAR");
    }

    [TestMethod]
    public void SupportsEmptyListReturnValue()
    {
        //An empty list is a NULL pointer in native code.
        ListTester.GetStringsTransferFullEmpty().Should().BeEmpty();
    }

    [TestMethod]
    public void SupportsRecordListReturnValueTransferFull()
    {
        //The records are owned by the caller, so they are adopted instead of copied.
        var records = ListTester.GetRecordsTransferFull();

        records.Should().HaveCount(2);
        records[0].GetRefCount().Should().Be(1);
        records[1].GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsRecordListReturnValueTransferContainer()
    {
        //Only the container is owned by the caller, so every record must be copied
        //to be able to outlive the list.
        var refCountBefore = ListTester.GetStaticRecordRefCount(0);

        var records = ListTester.GetRecordsTransferContainer();

        records.Should().HaveCount(2);
        ListTester.GetStaticRecordRefCount(0).Should().Be(refCountBefore + 1);
        records[0].GetRefCount().Should().Be(refCountBefore + 1);
    }

    [TestMethod]
    public void SupportsRecordListReturnValueTransferNone()
    {
        //Neither the container nor the records are owned by the caller, so every
        //record must be copied.
        var refCountBefore = ListTester.GetStaticRecordRefCount(1);

        var records = ListTester.GetRecordsTransferNone();

        records.Should().HaveCount(2);
        ListTester.GetStaticRecordRefCount(1).Should().Be(refCountBefore + 1);
    }
}
