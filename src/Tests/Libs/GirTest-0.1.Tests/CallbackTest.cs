using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class CallbackTest : Test
{
    [TestMethod]
    public void SupportsNotifiedCallbacks()
    {
        var tester = CallbackTester.New();

        tester.SetNotifiedCallback((val) => 2 * val);

        tester.RunNotifiedCallback(2, done: false).Should().Be(4);
        tester.RunNotifiedCallback(3, done: false).Should().Be(6);
        tester.RunNotifiedCallback(4, done: true).Should().Be(8);
        tester.RunNotifiedCallback(5, done: false).Should().Be(-1);
    }

    [TestMethod]
    public void SupportsCallbacksWithCallbacks()
    {
        var callbackCalled = false;

        void Callback(IntCallback callback)
        {
            callbackCalled = true;
            callback(5).Should().Be(5);
        }
        CallbackTester.RunCallbackWithMirrorInputCallback(Callback);

        callbackCalled.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbacksWithPointedPrimitiveValueTypes()
    {
        void Callback(ref int i)
        {
            i.Should().Be(42);
            i = 24;
        }

        var result = CallbackTester.RunCallbackReturnIntPointerValue(Callback);
        result.Should().Be(24);
    }

    [TestMethod]
    public void SupportsCallbacksWithOutPointer()
    {
        void Callback(out IntPtr ptr)
        {
            ptr = (IntPtr) 42;
        }

        var result = CallbackTester.RunCallbackOutPointer(Callback);
        result.Should().Be(42);
    }

    [TestMethod]
    public void CallbacksCanThrowAnException()
    {
        const string Message = "Test";
        void Callback()
        {
            throw new Exception(Message);
        }

        Action act = () => CallbackTester.RunCallbackError(Callback);
        act.Should().Throw<GLib.GException>().WithMessage(Message);
    }

    [TestMethod]
    public void SupportsCallbacksWithCallbacksThatRaiseAnError()
    {
        string? message = null;
        void Callback(CallbackError callbackError)
        {
            try
            {
                callbackError();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        CallbackTester.RunCallbackWithRaiseErrorCallback(Callback);
        message.Should().Be("CustomError");
    }

    [TestMethod]
    public void CallbacksCanThrowAnExceptionIfErrorIsNotLastParam()
    {
        const string Message = "Test";
        void Callback(int dummyValue)
        {
            throw new Exception(Message);
        }

        Action act = () => CallbackTester.RunCallbackErrorNotLastParam(Callback);
        act.Should().Throw<GLib.GException>().WithMessage(Message);
    }

    [TestMethod]
    public void SupportsCallbacksWithCallbacksThatRaiseAnErrorNotLastParam()
    {
        string? message = null;
        void Callback(CallbackErrorNotLastParam callbackError)
        {
            try
            {
                callbackError(42);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        CallbackTester.RunCallbackWithRaiseErrorCallbackNotLastParam(Callback);
        message.Should().Be("CustomError");
    }

    [TestMethod]
    public void SupportsCallbackWithTypeReturn()
    {
        GObject.Type Callback()
        {
            return GObject.Type.Boolean;
        }

        CallbackTester.RunCallbackWithTypeReturn(Callback).Should().Be(GObject.Type.Boolean);
    }

    [TestMethod]
    public void SupportsCallbackWithObjectReturn()
    {
        var testObject = ExecutorImpl.New();
        GObject.Object Callback()
        {
            return testObject;
        }

        var obj = CallbackTester.RunCallbackWithObjectReturn(Callback);
        obj.Should().Be(testObject);
    }

    [TestMethod]
    public void SupportsCallbackWithInterfaceReturn()
    {
        GirTest.Executor Callback()
        {
            return GirTest.ExecutorImpl.New();
        }

        CallbackTester.RunCallbackWithExecutorInterfaceReturn(Callback).Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbackWithOutPointedPrimitiveValueType()
    {
        void Callback(out int result)
        {
            result = 42;
        }

        CallbackTester.RunCallbackWithOutPointedPrimitiveValueType(Callback).Should().Be(42);
    }

    [TestMethod]
    public void SupportsCallbackWithPointedPrimitiveValueTypeAlias()
    {
        var myType = GObject.Type.Boolean;
        bool executed = false;

        void Callback(ref GObject.Type type)
        {
            type.Should().Be(myType);
            executed = true;
        }

        CallbackTester.RunCallbackWithPointedPrimitiveValueTypeAlias(Callback, ref myType);
        executed.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsCallbackWithConstantStringReturn()
    {
        var text = "Test";
        var constantString = new GLib.ConstantString(text);
        GLib.ConstantString Callback()
        {
            return constantString;
        }

        var result = CallbackTester.RunCallbackWithConstantStringReturn(Callback);
        result.Should().Be(text);
    }

    [TestMethod]
    public void SupportsCallbackWithNullableConstantStringReturn()
    {
        var text = "Test";
        var constantString = new GLib.ConstantString(text);
        GLib.ConstantString? Callback()
        {
            return constantString;
        }

        var result = CallbackTester.RunCallbackWithNullableConstantStringReturn(Callback);
        result.Should().Be(text);
    }

    [TestMethod]
    public void SupportsCallbackWithNullableConstantStringReturnAndNullValue()
    {
        GLib.ConstantString? Callback()
        {
            return null;
        }

        var result = CallbackTester.RunCallbackWithNullableConstantStringReturn(Callback);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsCallbacksWithNullableClassParameter()
    {
        var nullCalled = false;
        void NullCallback(CallbackTester? callbackTester)
        {
            callbackTester.Should().BeNull();
            nullCalled = true;
        }
        CallbackTester.RunCallbackWithNullableClassParameter(NullCallback, null);
        nullCalled.Should().BeTrue();

        var instanceCalled = false;
        var instance = CallbackTester.New();
        void InstanceCallback(CallbackTester? callbackTester)
        {
            callbackTester.Should().Be(instance);
            instanceCalled = true;
        }
        CallbackTester.RunCallbackWithNullableClassParameter(InstanceCallback, instance);
        instanceCalled.Should().BeTrue();
    }
}
