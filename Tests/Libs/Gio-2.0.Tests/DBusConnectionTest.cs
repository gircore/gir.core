using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gio.Tests
{
    [TestClass]
    public class DBusConnectionTest
    {
        [TestMethod]
        public void GetSessionBusShouldNotBeNull()
        {
            var obj = DBusConnection.Get(BusType.Session);
            obj.Should().NotBeNull();
        }
    }
}
