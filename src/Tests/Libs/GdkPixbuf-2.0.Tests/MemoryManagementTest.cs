using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class MemoryManagementTest
    {
        [TestMethod]
        public void TestAutomaticGObjectDisposal()
        {
            WeakReference weakReference = new(null);
            IDisposable? strongReference = null;

            void CreateInstance(bool keepInstance)
            {
                var obj = Pixbuf.NewFromFile("test.bmp");

                GObject.Native.ObjectMapper.ObjectCount.Should().Be(1);

                if (keepInstance)
                    strongReference = obj;

                weakReference.Target = obj;
            }

            CreateInstance(keepInstance: false);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GObject.Native.ObjectMapper.ObjectCount.Should().Be(0);
            weakReference.IsAlive.Should().BeFalse();
            strongReference.Should().BeNull();

            CreateInstance(keepInstance: true);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GObject.Native.ObjectMapper.ObjectCount.Should().Be(1);
            weakReference.IsAlive.Should().BeTrue();
            strongReference.Should().NotBeNull();

            // Cleanup: Dispose for other tests to work properly
            // It looks like the GC is not collecting the ObjectMapper data
            // if the GC.Collect() call is happening in the method which
            // contains the reference to be freed. There must be a context
            // switch before disposal happens.
            // This is the reason why we need to use the "CreateInstance"
            // method to create the instances which should be freed.
            // This behaviour is verified on Linux.
            strongReference.Dispose();
        }

        [TestMethod]
        public void TestManualGObjectDisposal()
        {
            var obj = Pixbuf.NewFromFile("test.bmp");
            GObject.Native.ObjectMapper.ObjectCount.Should().Be(1);
            obj.Dispose();
            GObject.Native.ObjectMapper.ObjectCount.Should().Be(0);
        }
    }
}
