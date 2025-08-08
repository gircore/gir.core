using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests.Classes;

[TestClass, TestCategory("UnitTest")]
public class ParamSpecTest : Test
{
    [TestMethod]
    public void CanCreateBooleanParamSpec()
    {
        var pspec = new ParamSpecBoolean(
            name: "test",
            nick: "test",
            blurb: "test",
            defaultValue: false,
            flags: ParamFlags.Writable
        );
        pspec.Handle.Should().NotBe(default);
    }
}
