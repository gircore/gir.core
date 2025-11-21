using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class PtrArrayTest
{
    [TestMethod]
    public void CanCreateNewPtrArray()
    {
        var ptrArray = PtrArray.New();
        ptrArray.Len.Should().Be(0);
    }
}
