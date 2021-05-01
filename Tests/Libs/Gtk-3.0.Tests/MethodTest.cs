using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests
{
    [TestClass, TestCategory("SystemTest")]
    public class MethodTest
    {
        [DataTestMethod]
        [DataRow("abc", "def")]
        [DataRow("öö", "ß")]
        public void TestStringArray(string value1, string value2)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.SetAuthors(new[] { value1, value2 });

            //TODO USE GET METHOD
            aboutDialog.Authors[0].Should().Be(value1);
            aboutDialog.Authors[1].Should().Be(value2);
        }
    }
}
