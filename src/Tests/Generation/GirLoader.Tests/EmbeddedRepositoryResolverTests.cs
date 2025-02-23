using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class EmbeddedRepositoryResolverTests
{
    [TestMethod]
    public void CanLoadRepositoryFromEmbeddedResource()
    {
        var resolver = new EmbeddedRepositoryResolver(
            typeof(EmbeddedRepositoryResolverTests).Assembly, "linux");

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository!.Namespace!.Name.Should().Be("GObject");
    }

    [TestMethod]
    public void ReturnsNullWhenEmbeddedResourceNotFound()
    {
        var resolver = new EmbeddedRepositoryResolver(
            typeof(EmbeddedRepositoryResolverTests).Assembly, "linux");

        var repository = resolver.ResolveRepository("TestLibrary-1.0.gir");

        repository.Should().BeNull();
    }
}
