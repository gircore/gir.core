using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class IntegerArrayTest : Test
{
    private const int Int0 = 1;
    private const int Int1 = 2;
    private const int Int2 = 3;
    private const nuint DataSize = 3;


    [TestMethod]
    public void ReturnIntArray()
    {
        var size = IntegerArrayTester.GetDataCount();
        size.Should().Be(DataSize);

        unsafe
        {
            var ptr = IntegerArrayTester.DataReturn();
            var span = new ReadOnlySpan<int>(ptr.ToPointer(), (int) size);
            span.Length.Should().Be((int) DataSize);
            span[0].Should().Be(Int0);
            span[1].Should().Be(Int1);
            span[2].Should().Be(Int2);
        }
    }

    [TestMethod]
    public void OutCallerAllocatesIntegerArrayBufferToBig()
    {
        var buffer = new int[5];
        var length = IntegerArrayTester.DataOutCallerAllocates(buffer);
        length.Should().Be((nint) DataSize);

        buffer[0].Should().Be(Int0);
        buffer[1].Should().Be(Int1);
        buffer[2].Should().Be(Int2);
        buffer[3].Should().Be(0); //Not filled by native code, buffer is bigger than needed
        buffer[4].Should().Be(0); //Not filled by native code, buffer is bigger than needed
    }

    [TestMethod]
    public void OutCallerAllocatesIntegerArrayBufferToSmall()
    {
        var buffer = new int[2];
        var length = IntegerArrayTester.DataOutCallerAllocates(buffer);
        length.Should().Be(2);

        buffer[0].Should().Be(Int0);
        buffer[1].Should().Be(Int1);
    }

    [TestMethod]
    public void CallbackReceivesIntegerArray()
    {
        var callbackInvoked = false;

        void Callback(Span<int> buf)
        {
            callbackInvoked = true;
            buf.Length.Should().Be((int) DataSize);
            buf[0].Should().Be(Int0);
            buf[1].Should().Be(Int1);
            buf[2].Should().Be(Int2);
        }

        IntegerArrayTester.InvokeCallback(Callback);
        callbackInvoked.Should().BeTrue();
    }

    [TestMethod]
    public void CallbackReceivesIntegerArrayWithoutLength()
    {
        var callbackInvoked = false;

        void Callback(ref int buf)
        {
            callbackInvoked = true;
            var span = MemoryMarshal.CreateSpan(ref buf, (int) IntegerArrayTester.GetDataCount());
            span.Length.Should().Be((int) DataSize);
            span[0].Should().Be(Int0);
            span[1].Should().Be(Int1);
            span[2].Should().Be(Int2);
        }

        IntegerArrayTester.InvokeCallbackNoLength(Callback);
        callbackInvoked.Should().BeTrue();
    }

    [TestMethod]
    public void IntegerArrayFromConstPointer()
    {
        var data = new int[] { 5, 6 };
        IntegerArrayTester.GetDataFromConstPointer(data).Should().Be(6);
    }

    [TestMethod]
    public void IntegerArrayWithGint64Size()
    {
        var data = new int[] { 5, 6 };
        IntegerArrayTester.GetDataWithGint64Size(data).Should().Be(6);
    }

    [TestMethod]
    public void SupportsInOutIntegerArray()
    {
        var data = new int[] { 1, 2, 3 };
        IntegerArrayTester.ClearData(data);
        data[0].Should().Be(0);
        data[1].Should().Be(0);
        data[2].Should().Be(0);
    }
}
