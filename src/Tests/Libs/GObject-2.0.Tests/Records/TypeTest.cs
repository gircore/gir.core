using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class TypeTest
{
    [TestMethod]
    public void SizeOfTypeIs8()
    {
        unsafe
        {
            sizeof(Type).Should().Be(8);
        }
    }
}
