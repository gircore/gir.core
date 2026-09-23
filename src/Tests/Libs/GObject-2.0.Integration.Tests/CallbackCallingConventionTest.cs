using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using DiagnosticAnalyzerTestProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class CallbackCallingConventionTest
{
    [TestMethod]
    [DataRow(typeof(SomeSubClass), 4)]
    [DataRow(typeof(SomeAbstractSubClass), 3)]
    public void GeneratedSubclassCallbacksUseCdecl(System.Type subclass, int expectedCallbacks)
    {
        var callbacks = subclass.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly)
            .Select(method => new { Method = method, Attribute = method.GetCustomAttribute<UnmanagedCallersOnlyAttribute>() })
            .Where(callback => callback.Attribute is not null)
            .ToArray();

        callbacks.Should().HaveCount(expectedCallbacks);
        foreach (var callback in callbacks)
            callback.Attribute!.CallConvs.Should().Equal([typeof(CallConvCdecl)],
                $"{subclass.Name}.{callback.Method.Name} is invoked through the GObject C ABI");
    }
}
