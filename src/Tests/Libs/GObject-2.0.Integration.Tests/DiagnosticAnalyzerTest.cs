using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{
    [TestMethod]
    public async Task ExpectedDiagnosticsAreRaised()
    {
        using var compiler = new Compiler("../../../../../Data/DiagnosticAnalyzerTestProject/DiagnosticAnalyzerTestProject.csproj");
        var project = await compiler.GetProjectAsync();
        var compilation = await project.GetCompilationAsync();

        var diagnostics = await compilation!
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync();

        diagnostics.ContainsDiagnosticForFile("GirCore1001", "RaiseGirCore1001.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1002", "RaiseGirCore1002.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1003", "RaiseGirCore1003.cs");

        diagnostics.ContainsNoDiagnosticForFile("GirCore1002", "NotRaiseGirCore1002.cs");
    }
}
