using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class RepositoryResolverFactoryTests
{
    [TestMethod]
    public void RepositoriesNotResolvedWithNullSearchPath()
    {
        var assembly = typeof(RepositoryResolverFactoryTests).Assembly;
        var factory = new RepositoryResolverFactory("linux", null, assembly);

        var resolver = factory.Create();
        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository.Should().BeNull();
    }

    [TestMethod]
    public void RepositoriesResolvedFromSearchPath()
    {
        using var directory = new DisposableTempDirectory();
        var filePath = Path.Join(directory.Path, "NewLibrary-1.0.gir");
        File.WriteAllText(filePath, InputRepositoryHelper.CreateXml("NewLibrary", "1.0"));
        var assembly = typeof(RepositoryResolverFactoryTests).Assembly;
        var factory = new RepositoryResolverFactory("linux", directory.Path, assembly);

        var resolver = factory.Create();
        var repository = resolver.ResolveRepository("NewLibrary-1.0.gir");

        repository!.Namespace!.Name.Should().Be("NewLibrary");
    }

    [TestMethod]
    public void RepositoriesResolvedFromEmbeddedFilesWhenSearchPathSet()
    {
        using var directory = new DisposableTempDirectory();
        var assembly = typeof(RepositoryResolverFactoryTests).Assembly;
        var factory = new RepositoryResolverFactory("linux", directory.Path, assembly);

        var resolver = factory.Create();
        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository!.Namespace!.Name.Should().Be("GObject");
    }
}
