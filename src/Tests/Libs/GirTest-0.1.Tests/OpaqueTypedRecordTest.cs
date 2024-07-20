using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class OpaqueTypedRecordTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        var recordTester = OpaqueTypedRecordCopyAnnotationTester.New();
        recordTester.Dispose();
    }

    [TestMethod]
    public void SupportsConstructorTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        recordTester.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);
    }

    [TestMethod]
    public void SupportsConstructorNullableTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.TryNew(false);
        recordTester!.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);

        var recordTester2 = OpaqueTypedRecordTester.TryNew(true);
        recordTester2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnValueTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        var recordTester2 = recordTester.Ref();
        recordTester2.GetRefCount().Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        var recordTester2 = recordTester.Mirror();

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var recordTester = OpaqueTypedRecordTester.New();
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
        var recordTester = OpaqueTypedRecordTester.New();
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
        var recordTester = OpaqueTypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsInstanceParameterTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        recordTester.TakeAndUnref();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        OpaqueTypedRecordTester.TakeAndUnrefFunc(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferFull()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        OpaqueTypedRecordTester.TakeAndUnrefFuncNullable(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);

        OpaqueTypedRecordTester.TakeAndUnrefFuncNullable(0, null);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferNone()
    {
        var result = OpaqueTypedRecordTester.TryGetRefCount(0, null);
        result.Should().Be(-1);

        result = OpaqueTypedRecordTester.TryGetRefCount(0, OpaqueTypedRecordTester.New());
        result.Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterArrayWithLengthParameter()
    {
        var recordTester1 = OpaqueTypedRecordTester.New();
        var recordTester2 = OpaqueTypedRecordTester.New();

        var result = OpaqueTypedRecordTester.GetRefCountSum(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        OpaqueTypedRecordTester.GetRefCountSum(Array.Empty<OpaqueTypedRecordTester>()).Should().Be(0);
    }

    [TestMethod]
    public void SupportsParameterNullableArrayWithLengthParameter()
    {
        var recordTester1 = OpaqueTypedRecordTester.New();
        var recordTester2 = OpaqueTypedRecordTester.New();

        var result = OpaqueTypedRecordTester.GetRefCountSumNullable(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        OpaqueTypedRecordTester.GetRefCountSumNullable(Array.Empty<OpaqueTypedRecordTester>()).Should().Be(0);
        OpaqueTypedRecordTester.GetRefCountSumNullable(null).Should().Be(-1);
    }

    [TestMethod]
    public void SupportsCallbackReturnNoOwnershipTransfer()
    {
        var recordTester = OpaqueTypedRecordTester.New();

        OpaqueTypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = OpaqueTypedRecordTester.RunCallbackReturnNoOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnNoOwnershipTransferNullable(bool useNull)
    {
        OpaqueTypedRecordTester? Create()
        {
            return useNull ? null : OpaqueTypedRecordTester.New();
        }

        var recordTester2 = OpaqueTypedRecordTester.RunCallbackReturnNoOwnershipTransferNullable(Create);
        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackReturnFullOwnershipTransfer()
    {
        var recordTester = OpaqueTypedRecordTester.New();

        OpaqueTypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = OpaqueTypedRecordTester.RunCallbackReturnFullOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnFullOwnershipTransferNullable(bool useNull)
    {
        OpaqueTypedRecordTester? Create()
        {
            return useNull ? null : OpaqueTypedRecordTester.New();
        }

        var recordTester2 = OpaqueTypedRecordTester.RunCallbackReturnFullOwnershipTransferNullable(Create);

        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackParameterFullOwnershipTransfer()
    {
        var called = false;

        void Callback(OpaqueTypedRecordTester recordTester)
        {
            recordTester.GetRefCount().Should().Be(1);
            called = true;
        }

        OpaqueTypedRecordTester.RunCallbackParameterFullOwnershipTransfer(Callback);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterFullOwnershipTransferNullable(bool useNull)
    {
        var called = false;

        void Callback(OpaqueTypedRecordTester? recordTester)
        {
            (recordTester is null).Should().Be(useNull);
            called = true;
        }

        OpaqueTypedRecordTester.RunCallbackParameterFullOwnershipTransferNullable(useNull, Callback);
        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbackParameterNoOwnershipTransfer()
    {
        var recordTester = OpaqueTypedRecordTester.New();
        var called = false;

        void Callback(OpaqueTypedRecordTester obj)
        {
            obj.GetRefCount().Should().Be(2);
            obj.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
            called = true;
        }

        OpaqueTypedRecordTester.RunCallbackParameterNoOwnershipTransfer(Callback, recordTester);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterNoOwnershipTransferNullable(bool useNull)
    {
        var recordTester = useNull ? null : OpaqueTypedRecordTester.New();
        var called = false;

        void Callback(OpaqueTypedRecordTester? obj)
        {
            (obj is null).Should().Be(useNull);

            if (!useNull)
                obj!.GetRefCount().Should().Be(2);

            called = true;
        }

        OpaqueTypedRecordTester.RunCallbackParameterNoOwnershipTransferNullable(Callback, recordTester);

        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsWrapHandle()
    {
        var recordTester = OpaqueTypedRecordTester.New();

        var wrapped = (OpaqueTypedRecordTester) GObject.Internal.BoxedWrapper.WrapHandle(
            handle: recordTester.Handle.DangerousGetHandle(),
            ownsHandle: false,
            gtype: OpaqueTypedRecordTester.GetGType()
        );

        wrapped.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
        recordTester.GetRefCount().Should().Be(2);
        wrapped.GetRefCount().Should().Be(2);
    }

    [TestMethod]
    public void IsSealed()
    {
        typeof(OpaqueTypedRecordTester).IsSealed.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsReferenceEquality()
    {
        var instance1 = OpaqueTypedRecordTester.New();
        var instance2 = OpaqueTypedRecordTester.New();

        instance1.Equals(instance1).Should().BeTrue();
        instance1.Equals(instance2).Should().BeFalse();

        var handle1 = instance1.Handle;
        var handle2 = instance2.Handle;
        var handle3 = handle1.OwnedCopy();

        handle1.Equals(handle1).Should().BeTrue();
        handle1.Equals(handle2).Should().BeFalse();
        handle1.Equals(handle3).Should().BeTrue();

        var instance3 = new OpaqueTypedRecordTester(handle1);
        instance1.Equals(instance3).Should().BeTrue();
    }

    [TestMethod]
    public void HasNativeEqualsMethod()
    {
        var instance1 = OpaqueTypedRecordTester.New();
        var instance2 = OpaqueTypedRecordTester.New();

        instance1.NativeEquals(instance2).Should().BeTrue();
    }
}
