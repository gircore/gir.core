using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class FontOptionsTest
    {
        [TestMethod]
        public void BindingsShouldSucceed()
        {
            var opts = new FontOptions();
            opts.Status.Should().Be(Status.Success);
        }
    }
}
