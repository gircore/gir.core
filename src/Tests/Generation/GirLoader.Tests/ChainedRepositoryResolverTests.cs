using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class ChainedRepositoryResolverTests
{
    [TestMethod]
    public void ReturnsNullWhenNoRepositoryFound()
    {
        var resolver = new ChainedRepositoryResolver(new[]
        {
            new StubRepositoryResolver(new Dictionary<string, Input.Repository>())
        });

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository.Should().BeNull();
    }

    [TestMethod]
    public void ReturnsFirstResolvedRepositoryWhenMultipleMatch()
    {
        var resolver = new ChainedRepositoryResolver(new[]
        {
            new StubRepositoryResolver(new Dictionary<string, Input.Repository>
            {
                {"GObject-2.0.gir", InputRepositoryHelper.CreateRepository("GObject", "2.1")},
            }),
            new StubRepositoryResolver(new Dictionary<string, Input.Repository>
            {
                {"GObject-2.0.gir", InputRepositoryHelper.CreateRepository("GObject", "2.2")},
            }),
        });

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository!.Namespace!.Version.Should().Be("2.1");
    }

    [TestMethod]
    public void ReturnsRepositoryFromFallbackResolver()
    {
        var resolver = new ChainedRepositoryResolver(new[]
        {
            new StubRepositoryResolver(new Dictionary<string, Input.Repository>
            {
                {"TestLibrary-1.0.gir", InputRepositoryHelper.CreateRepository("TestLibrary", "1.0")},
            }),
            new StubRepositoryResolver(new Dictionary<string, Input.Repository>
            {
                {"GObject-2.0.gir", InputRepositoryHelper.CreateRepository("GObject", "2.0")},
            }),
        });

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository!.Namespace!.Name.Should().Be("GObject");
    }

    private class StubRepositoryResolver : IRepositoryResolver
    {
        private readonly IReadOnlyDictionary<string, Input.Repository> _repositories;

        public StubRepositoryResolver(IReadOnlyDictionary<string, Input.Repository> repositories)
        {
            _repositories = repositories;
        }

        public Input.Repository? ResolveRepository(string fileName)
        {
            return _repositories.GetValueOrDefault(fileName);
        }
    }
}
