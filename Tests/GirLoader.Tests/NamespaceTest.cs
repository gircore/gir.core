using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test
{
    [TestClass, TestCategory("UnitTest")]
    public class NamespaceTest
    {
        [TestMethod]
        public void NameAndVersionShouldMatchInputModel()
        {
            var namespaceName = "MyNamespace";
            var version = "1.0";
            var inputRepository = Helper.GetInputRepository(namespaceName, version);

            var repository = Loader.Load(new [] { inputRepository }).First();
            repository.Namespace.Name.Value.Should().Be(namespaceName);
            repository.Namespace.Version.Should().Be(version);
        }
    }
}
