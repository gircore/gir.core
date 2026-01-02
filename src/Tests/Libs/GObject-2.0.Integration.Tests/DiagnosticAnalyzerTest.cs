using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{
    public TestContext TestContext { get; set; }

    private const string RaiseGirCore1001 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1001
                                            {
                                                public RaiseGirCore1001(string a) { }
                                            }
                                            """;

    private const string RaiseGirCore1002 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1002
                                            {
                                                public RaiseGirCore1002() { }
                                            }
                                            """;

    private const string RaiseGirCore1003 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1003
                                            {
                                                public RaiseGirCore1003(string a) : base(null!) { }
                                            }
                                            """;

    private const string RaiseGirCore1004 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1004<T>;
                                            """;

    private const string NotRaiseGirCore1002 = """
                                               [GObject.Subclass<GObject.Object>]
                                               public partial class NotRaiseGirCore1002
                                               {
                                                   static NotRaiseGirCore1002() { }
                                               }
                                               """;

    private const string NotRaiseGirCore1004 = """
                                               [GObject.Subclass<GObject.Object>]
                                               public partial class NotRaiseGirCore1004;
                                               
                                               public class NotRaiseGirCore1004<T> : NotRaiseGirCore1004;
                                               """;

    [TestMethod]
    [DataRow(RaiseGirCore1001, "GirCore1001", true)]
    [DataRow(RaiseGirCore1002, "GirCore1002", true)]
    [DataRow(RaiseGirCore1003, "GirCore1003", true)]
    [DataRow(RaiseGirCore1004, "GirCore1004", true)]
    [DataRow(NotRaiseGirCore1002, "GirCore1002", false)]
    [DataRow(NotRaiseGirCore1004, "GirCore1004", false)]
    public async Task ShouldRaiseExpectedDiagnosticIds(string code, string diagnosticId, bool diagnosticIdExpected)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(ShouldRaiseExpectedDiagnosticIds),
            syntaxTrees: [CSharpSyntaxTree.ParseText(code)],
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            references: [
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GObject.Object).Assembly.Location)
            ]
        );

        var driver = CSharpGeneratorDriver.Create(new SourceGenerator.Generator());
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _, TestContext.CancellationToken);

        var diagnostics = await outputCompilation
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync();

        if (diagnosticIdExpected)
            diagnostics.ContainsDiagnostic(diagnosticId);
        else
            diagnostics.ContainsNoDiagnostic(diagnosticId);
    }
}
