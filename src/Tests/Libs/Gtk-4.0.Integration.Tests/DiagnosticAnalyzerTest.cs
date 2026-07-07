using System.Linq;
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

    private const string DuplicateTemplateNameOne = """
                                                    namespace DuplicateTemplateName.One;

                                                    [GObject.Subclass<Gtk.Widget>]
                                                    [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                                    public partial class Widget;
                                                    """;

    private const string DuplicateTemplateNameTwo = """
                                                    namespace DuplicateTemplateName.Two;

                                                    [GObject.Subclass<Gtk.Widget>]
                                                    [Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
                                                    public partial class Widget;
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

    [TestMethod]
    public void ShouldGenerateNamespaceQualifiedHintNamesForDuplicateTemplateNames()
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(ShouldGenerateNamespaceQualifiedHintNamesForDuplicateTemplateNames),
            syntaxTrees: [
                CSharpSyntaxTree.ParseText(DuplicateTemplateNameOne),
                CSharpSyntaxTree.ParseText(DuplicateTemplateNameTwo)
            ],
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            references: [
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GObject.Object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Gtk.Widget).Assembly.Location)
            ]
        );

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SourceGenerator.Generator());
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var generatorDiagnostics, TestContext.CancellationToken);

        generatorDiagnostics.ContainsNoDiagnostic("CS8785");

        var generatedHintNames = driver
            .GetRunResult()
            .Results
            .SelectMany(x => x.GeneratedSources)
            .Select(x => x.HintName)
            .ToList();

        CollectionAssert.Contains(generatedHintNames, "DuplicateTemplateName.One.Widget.Template.g.cs");
        CollectionAssert.Contains(generatedHintNames, "DuplicateTemplateName.Two.Widget.Template.g.cs");
        CollectionAssert.AllItemsAreUnique(generatedHintNames);
    }
}
