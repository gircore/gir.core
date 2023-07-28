using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests;

[TestClass, TestCategory("UnitTest")]
public class MemoryManagementTest : Test
{
    [TestMethod]
    public void TestAutomaticGObjectDisposal()
    {
        WeakReference weakReference = new(null);
        IDisposable? strongReference = null;

        CollectAfter(() =>
        {
            var obj = Pixbuf.NewFromFile("test.bmp");
            GObject.Internal.ObjectMapper.ObjectCount.Should().Be(1);
            weakReference.Target = obj;
        });

        GObject.Internal.ObjectMapper.ObjectCount.Should().Be(0);
        weakReference.IsAlive.Should().BeFalse();

        CollectAfter(() =>
        {
            var obj = Pixbuf.NewFromFile("test.bmp");

            GObject.Internal.ObjectMapper.ObjectCount.Should().Be(1);

            strongReference = obj;
            weakReference.Target = obj;
        });

        weakReference.IsAlive.Should().BeTrue();
        strongReference.Should().NotBeNull();
    }

    [TestMethod]
    public void TestManualGObjectDisposal()
    {
        var obj = Pixbuf.NewFromFile("test.bmp");
        GObject.Internal.ObjectMapper.ObjectCount.Should().Be(1);
        obj.Dispose();
        GObject.Internal.ObjectMapper.ObjectCount.Should().Be(0);
    }
}
