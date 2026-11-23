using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class CallbackCallingConventionTest
{
    [TestMethod]
    [DataRow(typeof(Internal.SourceFunc))]
    [DataRow(typeof(Internal.DestroyNotify))]
    [DataRow(typeof(Internal.IOFuncsData.IoReadCallback))]
    [DataRow(typeof(Internal.IOFuncsData.IoCloseCallback))]
    public void NativeCallbackDelegateUsesCdecl(Type callbackType)
    {
        var attribute = callbackType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();

        attribute.Should().NotBeNull($"{callbackType} crosses the GLib C ABI");
        attribute!.CallingConvention.Should().Be(CallingConvention.Cdecl);
    }

    [TestMethod]
    public void SourceCallbackCanBeInvokedRepeatedlyByNativeMainContext()
    {
        using var context = MainContext.New();
        using var source = Functions.IdleSourceNew();
        var calls = 0;
        source.SetCallback(() => ++calls < 100);
        source.Attach(context);

        for (var i = 0; i < 100; i++)
            context.Iteration(false).Should().BeTrue($"callback iteration {i} must be dispatched");

        calls.Should().Be(100);
        source.IsDestroyed().Should().BeTrue();
        context.Iteration(false).Should().BeFalse("the callback returned false on its last invocation");
    }
}
