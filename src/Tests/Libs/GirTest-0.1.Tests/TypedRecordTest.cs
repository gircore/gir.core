using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class TypedRecordTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        var recordTester = TypedRecordTester.New();
        recordTester.Dispose();
    }

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
            gtype: Internal.TypedRecordTester.GetGType()
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

    [TestMethod]
    public void SupportsPrimitiveValueTypeField()
    {
        var instance = TypedRecordTester.New();
        instance.RefCount.Should().Be(1);
        instance.RefCount = 2;
        instance.RefCount.Should().Be(2);
        instance.RefCount = 1;
        instance.RefCount.Should().Be(1);
    }

    [TestMethod]
    public void SupportsEnumerationField()
    {
        var instance = TypedRecordTester.New();
        instance.CustomEnum.Should().Be(TypedRecordTesterEnum.A);
        instance.CustomEnum = TypedRecordTesterEnum.B;
        instance.CustomEnum.Should().Be(TypedRecordTesterEnum.B);
    }

    [TestMethod]
    public void SupportsBitfieldField()
    {
        var instance = TypedRecordTester.New();
        instance.CustomBitfield.Should().Be(TypedRecordTesterBitfield.Zero);
        instance.CustomBitfield = TypedRecordTesterBitfield.One;
        instance.CustomBitfield.Should().Be(TypedRecordTesterBitfield.One);
    }

    [TestMethod]
    public void SupportsStringField()
    {
        var instance = TypedRecordTester.New();
        instance.CustomString.Should().Be("Hello");
        instance.CustomString = "Test";
        instance.CustomString.Should().Be("Test");
        instance.CustomString = null;
        instance.CustomString.Should().BeNull();
    }

    [TestMethod]
    public void SupportsPrivateFields()
    {
        var data = new Internal.TypedRecordTesterData();
        data.CustomIntPrivate.Should().Be(0);

        //Private fields are not rendered in the public API
        typeof(TypedRecordTester).GetProperty(nameof(data.CustomIntPrivate)).Should().BeNull();
    }

    [TestMethod]
    public void HasParameterlessConstructor()
    {
        var instance = new TypedRecordTester();
        instance.Should().NotBeNull();
    }

    [TestMethod]
    public void IsSealed()
    {
        typeof(TypedRecordTester).IsSealed.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsReferenceEquality()
    {
        var handle1 = Internal.TypedRecordTesterManagedHandle.Create();
        var handle2 = Internal.TypedRecordTesterManagedHandle.Create();
        var handle3 = new Internal.TypedRecordTesterUnownedHandle(handle1.DangerousGetHandle());

        handle1.Equals(handle1).Should().BeTrue();
        handle1.Equals(handle2).Should().BeFalse();
        handle1.Equals(handle3).Should().BeTrue();

        var instance1 = new TypedRecordTester();
        var instance2 = new TypedRecordTester();
        var instance3 = new TypedRecordTester(handle1);
        var instance4 = new TypedRecordTester(handle1);

        instance1.Equals(instance1).Should().BeTrue();
        instance1.Equals(instance2).Should().BeFalse();
        instance3.Equals(instance4).Should().BeTrue();
    }

    [TestMethod]
    public void HasNativeEqualsMethod()
    {
        var instance1 = new TypedRecordTester();
        var instance2 = new TypedRecordTester();

        instance1.NativeEquals(instance2).Should().BeTrue();
    }
}
