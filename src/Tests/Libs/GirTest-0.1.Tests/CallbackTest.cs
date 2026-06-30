using System;
using AwesomeAssertions;
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

    [TestMethod]
    public void SupportsCallbackWithObjectReturnTransferFull()
    {
        var testObject = ExecutorImpl.New();

        GObject.Object Callback()
        {
            return testObject;
        }

        var refCount = CallbackTester.RunCallbackWithObjectReturnTransferFull(Callback);
        refCount.Should().Be(2); //One ref belongs to C, one to C#
    }

    [TestMethod]
    public void SupportsCallbackWithObjectReturnTransferNone()
    {
        var testObject = ExecutorImpl.New();
        GObject.Object Callback()
        {
            return testObject;
        }

        var refCount = CallbackTester.RunCallbackWithObjectReturnTransferNone(Callback);
        refCount.Should().Be(1);
    }

    [TestMethod]
    public void FullRoundTripTransferFullOutParameter()
    {
        var instance = CallbackTester.New();

        instance.RoundtripObjectRunCallbackOutTransferFull(Create);
        var obj = (ExecutorImpl) instance.RoundtripObjectGetInstance()!;
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferFull(TransferToCSharpRefCount1);
        obj.GetRefCount().Should().Be(1);
        instance.RoundtripObjectRunCallbackParameterTransferFull(TransferToCSharpNull);

        instance.RoundtripObjectRunCallbackOutTransferFull(CreateNull);
        instance.RoundtripObjectGetInstance().Should().BeNull();
        instance.RoundtripObjectRunCallbackOutTransferNone(CreateNull);
        instance.RoundtripObjectGetInstance().Should().BeNull();

        instance.RoundtripObjectRunCallbackOutTransferNone(Create);
        obj = (ExecutorImpl) instance.RoundtripObjectGetInstance()!;
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferNone(TransferToCSharpRefCount2);
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferNone(TransferToCSharpRefCount2);
        obj.GetRefCount().Should().Be(2);

        return;

        void TransferToCSharpNull(GObject.Object? obj)
        {
            obj.Should().BeNull();
        }

        void TransferToCSharpRefCount1(GObject.Object? obj)
        {
            var a = (ExecutorImpl) obj!;
            a.GetRefCount().Should().Be(1);
        }

        void TransferToCSharpRefCount2(GObject.Object? obj)
        {
            var a = (ExecutorImpl) obj!;
            a.GetRefCount().Should().Be(2);
        }

        void Create(out GObject.Object? obj)
        {
            var a = ExecutorImpl.New();
            a.GetRefCount().Should().Be(1);

            obj = a;
        }

        void CreateNull(out GObject.Object? obj)
        {
            obj = null;
        }
    }

    [TestMethod]
    public void FullRoundTripTransferFullReturn()
    {
        var instance = CallbackTester.New();

        instance.RoundtripObjectRunCallbackReturnTransferFull(Create);
        var obj = (ExecutorImpl) instance.RoundtripObjectGetInstance()!;
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferFull(TransferToCSharpRefCount1);
        obj.GetRefCount().Should().Be(1);
        instance.RoundtripObjectRunCallbackParameterTransferFull(TransferToCSharpNull);

        instance.RoundtripObjectRunCallbackReturnTransferNone(Create);
        obj = (ExecutorImpl) instance.RoundtripObjectGetInstance()!;
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferNone(TransferToCSharpRefCount2);
        obj.GetRefCount().Should().Be(2);
        instance.RoundtripObjectRunCallbackParameterTransferNone(TransferToCSharpRefCount2);
        obj.GetRefCount().Should().Be(2);

        return;

        void TransferToCSharpNull(GObject.Object? obj)
        {
            obj.Should().BeNull();
        }

        void TransferToCSharpRefCount1(GObject.Object? obj)
        {
            var a = (ExecutorImpl) obj!;
            a.GetRefCount().Should().Be(1);
        }

        void TransferToCSharpRefCount2(GObject.Object? obj)
        {
            var a = (ExecutorImpl) obj!;
            a.GetRefCount().Should().Be(2);
        }

        GObject.Object Create()
        {
            var a = ExecutorImpl.New();
            a.GetRefCount().Should().Be(1);

            return a;
        }
    }

    [TestMethod]
    public void SupportsCallbackWithEnumOutParameter()
    {
        void Callback(out CallbackTesterSimpleEnum e)
        {
            e = CallbackTesterSimpleEnum.Max;
        }

        var result = CallbackTester.RunCallbackEnumOut(Callback);
        result.Should().Be(CallbackTesterSimpleEnum.Max);
    }

    [TestMethod]
    public void SupportsCallbackWithEnumRefParameter()
    {
        void Callback(ref CallbackTesterSimpleEnum e)
        {
            if (e == CallbackTesterSimpleEnum.A)
                e = CallbackTesterSimpleEnum.B;
        }

        var result = CallbackTester.RunCallbackEnumRef(Callback);
        result.Should().Be(CallbackTesterSimpleEnum.B);
    }
}
