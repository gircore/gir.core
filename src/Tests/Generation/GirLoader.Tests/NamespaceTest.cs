using System.Linq;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class NamespaceTest
{
    [TestMethod]
    public void NameAndVersionShouldMatchInputModel()
    {
        var namespaceName = "MyNamespace";
        var version = "1.0";
        var inputRepository = InputRepositoryHelper.CreateRepository(namespaceName, version);

        var repository = new Loader(DummyResolver.Resolve).Load(new[] { inputRepository }).First();
        repository.Namespace.Name.Value.Should().Be(namespaceName);
        repository.Namespace.Version.Should().Be(version);
    }
}
