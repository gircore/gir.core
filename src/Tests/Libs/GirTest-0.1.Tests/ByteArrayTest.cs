using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ByteArrayTest : Test
{
    private const byte Byte0 = 0x00;
    private const byte Byte1 = 0x11;
    private const byte Byte2 = 0x22;
    private const nuint DataSize = 3;

    [TestMethod]
    public void SupportsByteArrayWithLengthInConstructor()
    {
        var data = new byte[] { 0x01 };
        _ = ByteArrayTester.NewFromData(data);
    }

    [TestMethod]
    public void ReturnByteArray()
    {
        var size = ByteArrayTester.GetDataSize();
        size.Should().Be(DataSize);

        unsafe
        {
            var ptr = ByteArrayTester.DataReturn();
            var span = new ReadOnlySpan<byte>(ptr.ToPointer(), (int) size);
            span.Length.Should().Be((int) DataSize);
            span[0].Should().Be(Byte0);
            span[1].Should().Be(Byte1);
            span[2].Should().Be(Byte2);
        }
    }

    [TestMethod]
    public void OutCallerAllocatesByteArrayBufferToBig()
    {
        var buffer = new byte[5];
        var length = ByteArrayTester.DataOutCallerAllocates(buffer);
        length.Should().Be((nint) DataSize);

        buffer[0].Should().Be(Byte0);
        buffer[1].Should().Be(Byte1);
        buffer[2].Should().Be(Byte2);
        buffer[3].Should().Be(0); //Not filled by native code, buffer is bigger than needed
        buffer[4].Should().Be(0); //Not filled by native code, buffer is bigger than needed
    }

    [TestMethod]
    public void OutCallerAllocatesByteArrayBufferToSmall()
    {
        var buffer = new byte[2];
        var length = ByteArrayTester.DataOutCallerAllocates(buffer);
        length.Should().Be(2);

        buffer[0].Should().Be(Byte0);
        buffer[1].Should().Be(Byte1);
    }

    [TestMethod]
    public void CallbackReceivesByteArray()
    {
        var callbackInvoked = false;

        void Callback(Span<byte> buf)
        {
            callbackInvoked = true;
            buf.Length.Should().Be((int) DataSize);
            buf[0].Should().Be(Byte0);
            buf[1].Should().Be(Byte1);
            buf[2].Should().Be(Byte2);
        }

        ByteArrayTester.InvokeCallback(Callback);
        callbackInvoked.Should().BeTrue();
    }

    [TestMethod]
    public void CallbackReceivesByteArrayWithoutLength()
    {
        var callbackInvoked = false;

        void Callback(ref byte buf)
        {
            callbackInvoked = true;
            var span = MemoryMarshal.CreateSpan(ref buf, (int) ByteArrayTester.GetDataSize());
            span.Length.Should().Be((int) DataSize);
            span[0].Should().Be(Byte0);
            span[1].Should().Be(Byte1);
            span[2].Should().Be(Byte2);
        }

        ByteArrayTester.InvokeCallbackNoLength(Callback);
        callbackInvoked.Should().BeTrue();
    }

    [TestMethod]
    public void ByteArrayFromConstPointer()
    {
        var data = new byte[] { 0x1, 0x2 };
        ByteArrayTester.GetDataFromConstPointer(data).Should().Be(0x2);
    }

    [TestMethod]
    public void SupportsInOutByteArray()
    {
        var data = new byte[] { 0x1, 0x2 };
        ByteArrayTester.ClearData(data);
        data[0].Should().Be(0);
        data[1].Should().Be(0);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void SupportsShrinkingArrays(int size)
    {
        var data = new byte[size];
        ByteArrayTester.RemoveLastArrayElement(data, out var newLength);
        newLength.Should().Be((nuint) size - 1);
    }
}
