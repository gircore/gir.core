using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class MemoryManagementTest
    {
        [TestMethod]
        public void UnownedHandleIsNotFreed()
        {
            CollectAfter(() => Dir.Open("..", 0));

            // Dir.Open creates a handle to a directory.
            // This handle does not transfer ownership. This
            // means we are not allowed to free the handle.
            // There is no method to free the handle, for this
            // reason we throw an exception if the handle is
            // tried to be freed.

            // The "CollectAfter" method ensures that the garbage collector
            // frees this handle as it is not used anymore. If the handle
            // would get freed, the exception would be raised and the 
            // unit test would fail.
        }

        private static void CollectAfter(Action action)
        {
            action();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
