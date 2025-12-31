using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task ExpectedDiagnosticsAreRaised()
    {
        using var compiler = new Compiler("../../../../../Data/DiagnosticAnalyzerTestProject/DiagnosticAnalyzerTestProject.csproj");
        var project = await compiler.GetProjectAsync();
        var compilation = await project.GetCompilationAsync(TestContext.CancellationToken);

        var diagnostics = await compilation!
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync();

        diagnostics.ContainsDiagnosticForFile("GirCore1001", "RaiseGirCore1001.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1002", "RaiseGirCore1002.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1003", "RaiseGirCore1003.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1004", "RaiseGirCore1004.cs");

        diagnostics.ContainsNoDiagnosticForFile("GirCore1002", "NotRaiseGirCore1002.cs");
        diagnostics.ContainsNoDiagnosticForFile("GirCore1004", "NotRaiseGirCore1004.cs");
    }
}
