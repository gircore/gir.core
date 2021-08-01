using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class ExceptionTest
    {
        [TestMethod]
        public void ErrorsRaiseAnException()
        {
            // To verify if the exception handling is working we try to open a directory which is not present.
            // This should always raise an exception.
            // Additionally we test if the name of the folder is contained inside the exception's message
            // to make sure the message gets converted correctly.

            var nameOfFolder = "MissingFolder";
            Action action = () => Dir.Open(nameOfFolder, 0);

            action.Should().Throw<Exception>().And.Message.Should().Contain(nameOfFolder);
        }

        [TestMethod]
        public void ErrorsAreNotAlwaysThrown()
        {
            //To verify if the exception handling is working we call a method which could potentially throw
            //an exception. We supply valid arguments and check that no exceptin is thrown.

            Dir.Open("..", 0).Should().NotBeNull();
        }
    }
}
