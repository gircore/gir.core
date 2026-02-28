using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class UntypedRecordTest : Test
{
    [TestMethod]
    public void SupportsConstructorTransferFull()
    {
        var recordTester = UntypedRecordTester.NewWithA(7);
        recordTester.A.Should().Be(7);
    }

    [TestMethod]
    public void SupportsInstanceParametersTransferNone()
    {
        var recordTester = new UntypedRecordTester { A = 17 };
        recordTester.GetA().Should().Be(17);
    }

    [TestMethod]
    public void SupportsOutParameterCallerAllocatesParameter()
    {
        UntypedRecordTester.OutParameterCallerAllocates(7, out var record);
        record.A.Should().Be(7);
    }

    [TestMethod]
    public void SupportsReturningTransferFull()
    {
        var record = UntypedRecordTester.NewWithA(12);
        record.A.Should().Be(12);
    }

    [TestMethod]
    public void SupportsCallbackOutParamterCallerAllocates()
    {
        void Create(out UntypedRecordTester tester)
        {
            tester = UntypedRecordTester.NewWithA(7);
        }

        var record = UntypedRecordTester.CallbackOutParameterCallerAllocates(Create);
        record.A.Should().Be(7);
    }

    [TestMethod]
    public void SupportsCallbackOutParamterCalleeAllocates()
    {
        void Create(out UntypedRecordTester tester)
        {
            tester = UntypedRecordTester.NewWithA(7);
        }

        var record = UntypedRecordTester.CallbackOutParameterCalleeAllocates(Create);
        record.A.Should().Be(7);
    }

    [TestMethod]
    public void SupportsConstructorNullableTransferFull()
    {
        var recordTester = UntypedRecordTester.TryNew(false);
        recordTester!.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);

        var recordTester2 = UntypedRecordTester.TryNew(true);
        recordTester2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterNullableTransferNone()
    {
        var record = UntypedRecordTester.NewWithA(1);
        UntypedRecordTester.GetANullable(7, record).Should().Be(1);

        UntypedRecordTester.GetANullable(7, null).Should().Be(7);
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var record = UntypedRecordTester.NewWithA(7);
        var mirror = record.Mirror();
        mirror.A.Should().Be(7);

        //Ensure a copy is created
        mirror.Handle.DangerousGetHandle().Should().NotBe(record.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var record = UntypedRecordTester.NewWithA(7);
        var mirror = record.NullableMirror(true);
        mirror!.A.Should().Be(7);

        //Ensure a copy is created
        mirror.Handle.DangerousGetHandle().Should().NotBe(record.Handle.DangerousGetHandle());

        var mirror2 = record.NullableMirror(false);
        mirror2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnValueTransferContainer()
    {
        // Container ownership is transferred to here and then released
        UntypedRecordContainerTester container = UntypedRecordTester.ReturnsTransferContainer();

        // Elements of container are not owned and copied
        UntypedRecordTester element1 = UntypedRecordTester.GetNthContainerData(container, 0);
        element1!.A.Should().Be(1);

        UntypedRecordTester element2 = UntypedRecordTester.GetNthContainerData(container, 1);
        element2!.A.Should().Be(2);
    }

    [TestMethod]
    public void SupportsRecordArrayAsParameter()
    {
        var array = new[] { UntypedRecordTester.NewWithA(7), UntypedRecordTester.NewWithA(6) };
        var result = UntypedRecordTester.GetAFromLastElement(array);
        result.Should().Be(6);
    }

    [TestMethod]
    public void SupportsRecordPointerArrayAsParameter()
    {
        var array = new[] { UntypedRecordTester.NewWithA(7), UntypedRecordTester.NewWithA(6) };
        var result = UntypedRecordTester.GetAFromLastElementPointer(array);
        result.Should().Be(6);
    }

    [TestMethod]
    public void IsSealed()
    {
        typeof(UntypedRecordTester).IsSealed.Should().BeTrue();
    }

    [TestMethod]
    public void HasNativeEqualsMethod()
    {
        var instance1 = new UntypedRecordTester();
        var instance2 = new UntypedRecordTester();

        instance1.NativeEquals(instance2).Should().BeTrue();
    }
}
