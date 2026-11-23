using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests;

[TestClass, TestCategory("UnitTest")]
public class CallbackCallingConventionTest
{
    [TestMethod]
    public void ToggleNotifyUsesCdecl()
    {
        var callbacks = typeof(Internal.InstanceCache)
            .GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            .Select(method => method.GetCustomAttribute<UnmanagedCallersOnlyAttribute>())
            .OfType<UnmanagedCallersOnlyAttribute>()
            .ToArray();

        callbacks.Should().NotBeEmpty("the instance cache registers a native toggle notification callback");
        foreach (var callback in callbacks)
            callback.CallConvs.Should().Equal(typeof(CallConvCdecl));
    }

    [TestMethod]
    public void ObjectClassVirtualFunctionsUseCdecl()
    {
        var fields = typeof(Internal.ObjectClassUnmanaged).GetFields()
            .Where(field => field.FieldType.IsFunctionPointer)
            .ToArray();

        fields.Should().HaveCount(8);
        foreach (var field in fields)
            field.GetModifiedFieldType().GetFunctionPointerCallingConventions()
                .Should().Equal([typeof(CallConvCdecl)], $"{field.Name} is called through the GObject C vtable");
    }

    [TestMethod]
    [DataRow(nameof(Internal.SubclassRegistrar.Register))]
    [DataRow(nameof(Internal.SubclassRegistrar.RegisterAbstract))]
    public void SubclassRegistrationAcceptsCdeclCallbacks(string methodName)
    {
        var method = typeof(Internal.SubclassRegistrar).GetMethod(methodName)!;
        var callbacks = method.GetParameters()
            .Where(parameter => parameter.ParameterType.IsFunctionPointer)
            .ToArray();

        callbacks.Should().HaveCount(2);
        foreach (var callback in callbacks)
            callback.GetModifiedParameterType().GetFunctionPointerCallingConventions()
                .Should().Equal([typeof(CallConvCdecl)], $"{callback.Name} is invoked by GObject during type initialization");
    }

    [TestMethod]
    public void ObjectCreationAndDisposalCanRepeatedlyInvokeToggleCallbacks()
    {
        for (var i = 0; i < 100; i++)
        {
            using var obj = Object.NewWithProperties([]);
            Internal.InstanceCache.TryGetObject(obj.Handle.DangerousGetHandle(), out var cached)
                .Should().BeTrue($"object {i} must remain in the instance cache while alive");
            cached.Should().BeSameAs(obj);
        }
    }
}
