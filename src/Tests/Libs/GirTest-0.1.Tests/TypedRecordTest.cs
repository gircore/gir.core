﻿using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class TypedRecordTest : Test
{
    [TestMethod]
    public void SupportsConstructorTransferFull()
    {
        var recordTester = TypedRecordTester.New();
        recordTester.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);
    }

    [TestMethod]
    public void SupportsConstructorNullableTransferFull()
    {
        var recordTester = TypedRecordTester.TryNew(false);
        recordTester!.Handle.DangerousGetHandle().Should().NotBe(IntPtr.Zero);

        var recordTester2 = TypedRecordTester.TryNew(true);
        recordTester2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnValueTransferFull()
    {
        var recordTester = TypedRecordTester.New();
        var recordTester2 = recordTester.Ref();
        recordTester2.GetRefCount().Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var recordTester = TypedRecordTester.New();
        var recordTester2 = recordTester.Mirror();

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var recordTester = TypedRecordTester.New();
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
        var recordTester = TypedRecordTester.New();
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
        var recordTester = TypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsInstanceParameterTransferFull()
    {
        var recordTester = TypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        recordTester.TakeAndUnref();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var recordTester = TypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        TypedRecordTester.TakeAndUnrefFunc(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferFull()
    {
        var recordTester = TypedRecordTester.New();
        recordTester.GetRefCount().Should().Be(1);
        TypedRecordTester.TakeAndUnrefFuncNullable(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);

        TypedRecordTester.TakeAndUnrefFuncNullable(0, null);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferNone()
    {
        var result = TypedRecordTester.TryGetRefCount(0, null);
        result.Should().Be(-1);

        result = TypedRecordTester.TryGetRefCount(0, TypedRecordTester.New());
        result.Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterArrayWithLengthParameter()
    {
        var recordTester1 = TypedRecordTester.New();
        var recordTester2 = TypedRecordTester.New();

        var result = TypedRecordTester.GetRefCountSum(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        TypedRecordTester.GetRefCountSum(Array.Empty<TypedRecordTester>()).Should().Be(0);
    }

    [TestMethod]
    public void SupportsParameterNullableArrayWithLengthParameter()
    {
        var recordTester1 = TypedRecordTester.New();
        var recordTester2 = TypedRecordTester.New();

        var result = TypedRecordTester.GetRefCountSumNullable(new[] { recordTester1, recordTester2 });
        result.Should().Be(2);

        TypedRecordTester.GetRefCountSumNullable(Array.Empty<TypedRecordTester>()).Should().Be(0);
        TypedRecordTester.GetRefCountSumNullable(null).Should().Be(-1);
    }

    [TestMethod]
    public void SupportsCallbackReturnNoOwnershipTransfer()
    {
        var recordTester = TypedRecordTester.New();

        TypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = TypedRecordTester.RunCallbackReturnNoOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnNoOwnershipTransferNullable(bool useNull)
    {
        TypedRecordTester? Create()
        {
            return useNull ? null : TypedRecordTester.New();
        }

        var recordTester2 = TypedRecordTester.RunCallbackReturnNoOwnershipTransferNullable(Create);
        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackReturnFullOwnershipTransfer()
    {
        var recordTester = TypedRecordTester.New();

        TypedRecordTester Create()
        {
            return recordTester;
        }

        var recordTester2 = TypedRecordTester.RunCallbackReturnFullOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnFullOwnershipTransferNullable(bool useNull)
    {
        TypedRecordTester? Create()
        {
            return useNull ? null : TypedRecordTester.New();
        }

        var recordTester2 = TypedRecordTester.RunCallbackReturnFullOwnershipTransferNullable(Create);

        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackParameterFullOwnershipTransfer()
    {
        var called = false;

        void Callback(TypedRecordTester recordTester)
        {
            recordTester.GetRefCount().Should().Be(1);
            called = true;
        }

        TypedRecordTester.RunCallbackParameterFullOwnershipTransfer(Callback);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterFullOwnershipTransferNullable(bool useNull)
    {
        var called = false;

        void Callback(TypedRecordTester? recordTester)
        {
            (recordTester is null).Should().Be(useNull);
            called = true;
        }

        TypedRecordTester.RunCallbackParameterFullOwnershipTransferNullable(useNull, Callback);
        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbackParameterNoOwnershipTransfer()
    {
        var recordTester = TypedRecordTester.New();
        var called = false;

        void Callback(TypedRecordTester obj)
        {
            obj.GetRefCount().Should().Be(2);
            obj.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
            called = true;
        }

        TypedRecordTester.RunCallbackParameterNoOwnershipTransfer(Callback, recordTester);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterNoOwnershipTransferNullable(bool useNull)
    {
        var recordTester = useNull ? null : TypedRecordTester.New();
        var called = false;

        void Callback(TypedRecordTester? obj)
        {
            (obj is null).Should().Be(useNull);

            if (!useNull)
                obj!.GetRefCount().Should().Be(2);

            called = true;
        }

        TypedRecordTester.RunCallbackParameterNoOwnershipTransferNullable(Callback, recordTester);

        called.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsWrapHandle()
    {
        var recordTester = TypedRecordTester.New();

        var wrapped = (TypedRecordTester) GObject.Internal.BoxedWrapper.WrapHandle(
            handle: recordTester.Handle.DangerousGetHandle(),
            ownsHandle: false,
            gtype: TypedRecordTester.GetGType()
        );

        wrapped.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
        recordTester.GetRefCount().Should().Be(2);
        wrapped.GetRefCount().Should().Be(2);
    }

    [TestMethod]
    public void SupportsCallbackWithOutParameter()
    {
        var instance = TypedRecordTester.New();

        void Callback(out TypedRecordTester? recordTester)
        {
            recordTester = instance;
        }

        var result = TypedRecordTester.RunCallbackCreateNullableFullOwnershipTransferOut(Callback);
        result!.Handle.DangerousGetHandle().Should().Be(instance.Handle.DangerousGetHandle());
    }
}