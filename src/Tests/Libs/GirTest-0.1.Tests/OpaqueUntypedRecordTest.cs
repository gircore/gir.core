using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class OpaqueUntypedRecordTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.Dispose();
    }

    [TestMethod]
    public void SupportsConstructorTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);
    }

    [TestMethod]
    public void SupportsConstructorNullableTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.TryNew(false);
        recordTester!.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);

        var recordTester2 = OpaqueUntypedRecordTester.TryNew(true);
        recordTester2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnValueTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        var recordTester2 = recordTester.Ref();
        recordTester2.GetRefCount().Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        var recordTester2 = recordTester.Mirror();

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        var recordTester2 = recordTester.NullableMirror(true);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2!.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());

        var recordTester3 = recordTester.NullableMirror(false);
        recordTester3.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        var recordTester2 = recordTester.TryRef(false);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2!.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());

        var recordTester3 = recordTester.TryRef(true);
        recordTester3.Should().BeNull();
    }

    [TestMethod]
    public void SupportsInstanceParameterTransferNone()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsInstanceParameterTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        recordTester.TakeAndUnref();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        OpaqueUntypedRecordTester.TakeAndUnrefFunc(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferFull()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        OpaqueUntypedRecordTester.TakeAndUnrefFuncNullable(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);

        OpaqueUntypedRecordTester.TakeAndUnrefFuncNullable(0, null);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferNone()
    {
        var result = OpaqueUntypedRecordTester.TryGetRefCount(0, null);
        result.Should().Be(-1);

        result = OpaqueUntypedRecordTester.TryGetRefCount(0, OpaqueUntypedRecordTester.New());
        result.Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterArrayWithLengthParameter()
    {
        var recordTester1 = OpaqueUntypedRecordTester.New();
        var recordTester2 = OpaqueUntypedRecordTester.New();

        var result = OpaqueUntypedRecordTester.GetRefCountSum(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        OpaqueUntypedRecordTester.GetRefCountSum(Array.Empty<OpaqueUntypedRecordTester>()).Should().Be(0);
    }

    [TestMethod]
    public void SupportsParameterNullableArrayWithLengthParameter()
    {
        var recordTester1 = OpaqueUntypedRecordTester.New();
        var recordTester2 = OpaqueUntypedRecordTester.New();

        var result = OpaqueUntypedRecordTester.GetRefCountSumNullable(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        OpaqueUntypedRecordTester.GetRefCountSumNullable(Array.Empty<OpaqueUntypedRecordTester>()).Should().Be(0);
        OpaqueUntypedRecordTester.GetRefCountSumNullable(null).Should().Be(-1);
    }

    [TestMethod]
    public void SupportsCallbackReturnNoOwnershipTransfer()
    {
        var recordTester = OpaqueUntypedRecordTester.New();

        OpaqueUntypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = OpaqueUntypedRecordTester.RunCallbackReturnNoOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnNoOwnershipTransferNullable(bool useNull)
    {
        OpaqueUntypedRecordTester? Create()
        {
            return useNull ? null : OpaqueUntypedRecordTester.New();
        }

        var recordTester2 = OpaqueUntypedRecordTester.RunCallbackReturnNoOwnershipTransferNullable(Create);
        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackReturnFullOwnershipTransfer()
    {
        var recordTester = OpaqueUntypedRecordTester.New();

        OpaqueUntypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = OpaqueUntypedRecordTester.RunCallbackReturnFullOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnFullOwnershipTransferNullable(bool useNull)
    {
        OpaqueUntypedRecordTester? Create()
        {
            return useNull ? null : OpaqueUntypedRecordTester.New();
        }

        var recordTester2 = OpaqueUntypedRecordTester.RunCallbackReturnFullOwnershipTransferNullable(Create);

        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackParameterFullOwnershipTransfer()
    {
        var called = false;

        void Callback(OpaqueUntypedRecordTester recordTester)
        {
            recordTester.GetRefCount().Should().Be(1);
            called = true;
        }

        OpaqueUntypedRecordTester.RunCallbackParameterFullOwnershipTransfer(Callback);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterFullOwnershipTransferNullable(bool useNull)
    {
        var called = false;

        void Callback(OpaqueUntypedRecordTester? recordTester)
        {
            (recordTester is null).Should().Be(useNull);
            called = true;
        }

        OpaqueUntypedRecordTester.RunCallbackParameterFullOwnershipTransferNullable(useNull, Callback);
        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbackParameterNoOwnershipTransfer()
    {
        var recordTester = OpaqueUntypedRecordTester.New();
        var called = false;

        void Callback(OpaqueUntypedRecordTester obj)
        {
            obj.GetRefCount().Should().Be(2);
            obj.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
            called = true;
        }

        OpaqueUntypedRecordTester.RunCallbackParameterNoOwnershipTransfer(Callback, recordTester);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterNoOwnershipTransferNullable(bool useNull)
    {
        var recordTester = useNull ? null : OpaqueUntypedRecordTester.New();
        var called = false;

        void Callback(OpaqueUntypedRecordTester? obj)
        {
            (obj is null).Should().Be(useNull);

            if (!useNull)
                obj!.GetRefCount().Should().Be(2);

            called = true;
        }

        OpaqueUntypedRecordTester.RunCallbackParameterNoOwnershipTransferNullable(Callback, recordTester);

        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsWrapHandle()
    {
        //TODO: Depends on https://github.com/gircore/gir.core/issues/946
        Assert.Inconclusive();
    }

    [TestMethod]
    public void IsSealed()
    {
        typeof(OpaqueUntypedRecordTester).IsSealed.Should().BeTrue();
    }

    [TestMethod]
    public void HasNativeEqualsMethod()
    {
        var instance1 = OpaqueUntypedRecordTester.New();
        var instance2 = OpaqueUntypedRecordTester.New();

        instance1.NativeEquals(instance2).Should().BeTrue();
    }
}
