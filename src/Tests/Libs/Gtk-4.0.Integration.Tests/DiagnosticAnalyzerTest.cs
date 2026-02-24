using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{
    public TestContext TestContext { get; set; }

    private const string RaiseGirCore2001 = """
                                            [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                            public partial class RaiseGirCore2001;
                                            """;

    private const string NotRaiseGirCore2001 = """
                                               [GObject.Subclass<GObject.Object>]
                                               [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                               public partial class NotRaiseGirCore2001;
                                               """;

    private const string RaiseGirCore2002 = """
                                            [GObject.Subclass<GObject.Object>]
                                            [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                            public partial class RaiseGirCore2002;
                                            """;

    private const string NotRaiseGirCore2002 = """
                                               [GObject.Subclass<Gtk.Widget>]
                                               [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                               public partial class NotRaiseGirCore2002;
                                               """;

    private const string RaiseGirCore2003 = """
                                            public partial class RaiseGirCore2003
                                            {
                                                [Gtk.Connect]
                                                private Gtk.Label label;
                                            }
                                            """;

    private const string NotRaiseGirCore2003 = """
                                            [Gtk.Template<Gtk.AssemblyResource>("Test.ui")]
                                            public partial class RaiseGirCore2003
                                            {
                                                [Gtk.Connect]
                                                private Gtk.Label label;
                                            }
                                            """;

    [TestMethod]
    [DataRow(RaiseGirCore2001, "GirCore2001", true)]
    [DataRow(NotRaiseGirCore2001, "GirCore2001", false)]
    [DataRow(RaiseGirCore2002, "GirCore2002", true)]
    [DataRow(NotRaiseGirCore2002, "GirCore2002", false)]
    [DataRow(RaiseGirCore2003, "GirCore2003", true)]
    [DataRow(NotRaiseGirCore2003, "GirCore2003", false)]
    public async Task ShouldRaiseExpectedDiagnosticIds(string code, string diagnosticId, bool diagnosticIdExpected)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(ShouldRaiseExpectedDiagnosticIds),
            syntaxTrees: [CSharpSyntaxTree.ParseText(code)],
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            references: [
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GObject.Object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Gtk.Widget).Assembly.Location)
            ]
        );

        var driver = CSharpGeneratorDriver.Create(new SourceGenerator.Generator());
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _, TestContext.CancellationToken);

        var diagnostics = await outputCompilation
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync(TestContext.CancellationToken);

        if (diagnosticIdExpected)
            diagnostics.ContainsDiagnostic(diagnosticId);
        else
            diagnostics.ContainsNoDiagnostic(diagnosticId);
    }
}
