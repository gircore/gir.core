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

    [TestMethod]
    public void SupportsCallbackParameterInOutValueChange()
    {
        IntPtr? handle1 = null;

        void Callback(ref UntypedRecordTester? tester)
        {
            tester!.A.Should().Be(42);
            tester.A = 24;
            handle1 = tester.Handle.DangerousGetHandle();
        }

        var instance = UntypedRecordTester.RunCallbackParameterCallbackParameterInout(Callback);
        instance!.A.Should().Be(24);

        handle1.Should().NotBe(instance.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsCallbackParameterInOutValueNull()
    {
        void Callback(ref UntypedRecordTester? tester)
        {
            tester.Should().NotBeNull();
            tester = null;
        }

        var instance2 = UntypedRecordTester.RunCallbackParameterCallbackParameterInout(Callback);
        instance2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsCallbackParameterInOutDifferentInstance()
    {
        UntypedRecordTester? instance1 = null;
        UntypedRecordTester? instance2 = null;

        void Callback(ref UntypedRecordTester? tester)
        {
            tester!.A.Should().Be(42);

            instance1 = tester;
            instance2 = tester = UntypedRecordTester.NewWithA(24);
        }

        var instance3 = UntypedRecordTester.RunCallbackParameterCallbackParameterInout(Callback);
        instance3!.A.Should().Be(24);

        ReferenceEquals(instance1, instance2).Should().BeFalse();
        ReferenceEquals(instance2, instance3).Should().BeFalse();
        ReferenceEquals(instance3, instance1).Should().BeFalse();
    }
}
