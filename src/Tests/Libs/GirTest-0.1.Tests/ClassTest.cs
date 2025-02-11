using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ClassTest : Test
{
    [TestMethod]
    public void CanDisposeInstanceAfterOwnershipIsTransferredAndUnrefed()
    {
        var obj = ClassTester.New();
        ClassTester.TransferOwnershipFullAndUnref(obj);
        var act = () => obj.Dispose();
        act.Should().NotThrow();
    }

    [TestMethod]
    public void InstanceIsGarbageCollectedAfterOwnershipTransferAndUnref()
    {
        var reference = new System.WeakReference(null);

        CollectAfter(() =>
        {
            var obj = ClassTester.New();
            reference.Target = obj;
            ClassTester.TransferOwnershipFullAndUnref(obj);
        });

        reference.IsAlive.Should().BeFalse();
    }

    [TestMethod]
    public void CanReturnHiddenInstance()
    {
        //Ensure that return type of "CreateHiddenInstance" is "GObject.Object"
        typeof(ClassTester).GetMethod(nameof(ClassTester.CreateHiddenInstance)).Should().Return<GObject.Object>();

        //Ensure that returned instance actually is a "ClassTester"
        var instance = ClassTester.CreateHiddenInstance();
        instance.Should().BeOfType<ClassTester>();
    }

    [TestMethod]
    public void CanTransferOwnershipOfInterfaces()
    {
        var obj = ClassTester.New();

        var executor = GirTest.ExecutorImpl.New();
        var instanceData = Marshal.PtrToStructure<GObject.Internal.ObjectData>(executor.Handle.DangerousGetHandle());
        instanceData.RefCount.Should().Be(1);

        obj.TakeExecutor(executor);
        instanceData = Marshal.PtrToStructure<GObject.Internal.ObjectData>(executor.Handle.DangerousGetHandle());
        instanceData.RefCount.Should().Be(2);

        obj.FreeExecutor();
        instanceData = Marshal.PtrToStructure<GObject.Internal.ObjectData>(executor.Handle.DangerousGetHandle());
        instanceData.RefCount.Should().Be(1);
    }

    [TestMethod]
    public void GObjectDisposeMethodIsVirtual()
    {
        typeof(GObject.Object).GetMethod(nameof(GObject.Object.Dispose)).Should().BeVirtual();
    }

    [TestMethod]
    public void TestManualGObjectDisposal()
    {
        var obj = ClassTester.New();
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
        obj.Dispose();
        GObject.Internal.InstanceCache.ObjectCount.Should().Be(0);
    }

    [TestMethod]
    public void TestAutomaticGObjectDisposal()
    {
        WeakReference weakReference = new(null);
        IDisposable? strongReference = null;

        CollectAfter(() =>
        {
            var obj = ClassTester.New();
            GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);
            weakReference.Target = obj;
        });

        GObject.Internal.InstanceCache.ObjectCount.Should().Be(0);
        weakReference.IsAlive.Should().BeFalse();

        CollectAfter(() =>
        {
            var obj = ClassTester.New();

            GObject.Internal.InstanceCache.ObjectCount.Should().Be(1);

            strongReference = obj;
            weakReference.Target = obj;
        });

        weakReference.IsAlive.Should().BeTrue();
        strongReference.Should().NotBeNull();
    }
}
