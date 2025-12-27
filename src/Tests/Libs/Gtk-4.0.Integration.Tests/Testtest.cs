using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass]
public class Testtest
{
    [TestMethod]
    public void Test1()
    {

        var code = """
                   [GObject.Subclass<Gtk.Box>]
                   [Gtk.Template("file.ui")]
                   public partial class CompositeBoxWidget
                   {
                       public void Bla()
                       {
                   
                       }
                   }
                   """;
        
        var generator = new Gtk.Integration.SourceGenerator.Generator();

        var driver = CSharpGeneratorDriver.Create(generator);

        var compilation = CSharpCompilation.Create(nameof(Test1),
            [
                CSharpSyntaxTree.ParseText(code),
            ],
            [
                Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            ]);

        var runResult = driver.RunGenerators(compilation).GetRunResult();
    }
}
