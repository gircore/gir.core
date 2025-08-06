using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class TypedRecordCopyAnnotationTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        recordTester.Dispose();
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        var recordTester2 = recordTester.Mirror();

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        var recordTester2 = recordTester.NullableMirror(true);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2!.GetRefCount().Should().Be(2);

        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());

        var recordTester3 = recordTester.NullableMirror(false);
        recordTester3.Should().BeNull();
    }

    [TestMethod]
    public void SupportsInstanceParameterTransferFull()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        recordTester.GetRefCount().Should().Be(1);
        recordTester.TakeAndUnref();
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        recordTester.GetRefCount().Should().Be(1);
        TypedRecordCopyAnnotationTester.TakeAndUnrefFunc(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);
    }

    [TestMethod]
    public void SupportsParameterNullableTransferFull()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        recordTester.GetRefCount().Should().Be(1);
        TypedRecordCopyAnnotationTester.TakeAndUnrefFuncNullable(0, recordTester);
        recordTester.GetRefCount().Should().Be(1);

        TypedRecordCopyAnnotationTester.TakeAndUnrefFuncNullable(0, null);
    }

    [TestMethod]
    public void SupportsCallbackReturnNoOwnershipTransfer()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();

        TypedRecordCopyAnnotationTester Create()
        {
            return recordTester;
        }

        var recordTester2 = TypedRecordCopyAnnotationTester.RunCallbackReturnNoOwnershipTransfer(Create);

        recordTester.GetRefCount().Should().Be(2);
        recordTester2.GetRefCount().Should().Be(2);
        recordTester.Handle.DangerousGetHandle().Should().Be(recordTester2.Handle.DangerousGetHandle());
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackReturnNoOwnershipTransferNullable(bool useNull)
    {
        TypedRecordCopyAnnotationTester? Create()
        {
            return useNull ? null : TypedRecordCopyAnnotationTester.New();
        }

        var recordTester2 = TypedRecordCopyAnnotationTester.RunCallbackReturnNoOwnershipTransferNullable(Create);
        if (useNull)
            recordTester2.Should().BeNull();
        else
            recordTester2.Should().NotBeNull();
    }

    [TestMethod]
    public void SupportsCallbackParameterNoOwnershipTransfer()
    {
        var recordTester = TypedRecordCopyAnnotationTester.New();
        var called = false;

        void Callback(TypedRecordCopyAnnotationTester obj)
        {
            obj.GetRefCount().Should().Be(2);
            obj.Handle.DangerousGetHandle().Should().Be(recordTester.Handle.DangerousGetHandle());
            called = true;
        }

        TypedRecordCopyAnnotationTester.RunCallbackParameterNoOwnershipTransfer(Callback, recordTester);

        called.Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsCallbackParameterNoOwnershipTransferNullable(bool useNull)
    {
        var recordTester = useNull ? null : TypedRecordCopyAnnotationTester.New();
        var called = false;

        void Callback(TypedRecordCopyAnnotationTester? obj)
        {
            (obj is null).Should().Be(useNull);

            if (!useNull)
                obj!.GetRefCount().Should().Be(2);

            called = true;
        }

        TypedRecordCopyAnnotationTester.RunCallbackParameterNoOwnershipTransferNullable(Callback, recordTester);

        called.Should().BeTrue();
    }
}
