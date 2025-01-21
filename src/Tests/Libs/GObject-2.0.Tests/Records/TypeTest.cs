using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests;

[TestClass, TestCategory("UnitTest")]
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

    [TestMethod]
    public void FundamentalTypesCanBeDetected()
    {
        //(255 << 2) => G_TYPE_FUNDAMENTAL_MAX
        Internal.Functions.IsFundamental(255 << 2).Should().BeTrue();

        //Fundamental types
        Internal.Functions.IsFundamental(Internal.Object.GetGType()).Should().BeTrue();

        //Non fundamental types
        Internal.Functions.IsFundamental(Internal.Binding.GetGType()).Should().BeFalse();
    }
}
