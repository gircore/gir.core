using System.IO;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class DirectoryRepositoryResolverTests
{
    [TestMethod]
    public void CanLoadRepositoryFromDirectory()
    {
        using var directory = new DisposableTempDirectory();
        var filePath = Path.Join(directory.Path, "GObject-2.0.gir");
        File.WriteAllText(filePath, InputRepositoryHelper.CreateXml("GObject", "2.0"));

        var resolver = new DirectoryRepositoryResolver(directory.Path);

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository!.Namespace!.Name.Should().Be("GObject");
    }

    [TestMethod]
    public void ReturnsNullWhenFileNotFound()
    {
        using var directory = new DisposableTempDirectory();
        var resolver = new DirectoryRepositoryResolver(directory.Path);

        var repository = resolver.ResolveRepository("GObject-2.0.gir");

        repository.Should().BeNull();
    }
}
