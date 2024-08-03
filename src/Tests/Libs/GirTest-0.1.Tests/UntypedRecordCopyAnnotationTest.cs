using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class UntypedRecordCopyAnnotationTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        var recordTester = new UntypedRecordCopyAnnotationTester();
        recordTester.Dispose();
    }

    [TestMethod]
    public void SupportsCallbackOutParamterCalleeAllocates()
    {
        void Create(out UntypedRecordCopyAnnotationTester tester)
        {
            tester = UntypedRecordCopyAnnotationTester.NewWithA(7);
        }

        var record = UntypedRecordCopyAnnotationTester.CallbackOutParameterCalleeAllocates(Create);
        record.A.Should().Be(7);
    }

    [TestMethod]
    public void SupportsReturnValueTransferNone()
    {
        var record = UntypedRecordCopyAnnotationTester.NewWithA(7);
        var mirror = record.Mirror();
        mirror.A.Should().Be(7);

        //Ensure a copy is created
        mirror.Handle.DangerousGetHandle().Should().NotBe(record.Handle.DangerousGetHandle());
    }

    [TestMethod]
    public void SupportsReturnValueNullableTransferNone()
    {
        var record = UntypedRecordCopyAnnotationTester.NewWithA(7);
        var mirror = record.NullableMirror(true);
        mirror!.A.Should().Be(7);

        //Ensure a copy is created
        mirror.Handle.DangerousGetHandle().Should().NotBe(record.Handle.DangerousGetHandle());

        var mirror2 = record.NullableMirror(false);
        mirror2.Should().BeNull();
    }

    [TestMethod]
    public void IsSealed()
    {
        typeof(UntypedRecordCopyAnnotationTester).IsSealed.Should().BeTrue();
    }
}
