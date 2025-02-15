using System.Linq;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{

    [TestMethod]
    public async Task ExpectedDiagnosticsAreRaised()
    {
        var manager = new AnalyzerManager();
        var analyzer = manager.GetProject("../../../../../Data/DiagnosticAnalyzerTestProject/DiagnosticAnalyzerTestProject.csproj");
        using var workspace = analyzer.GetWorkspace();
        var project = workspace.CurrentSolution.Projects.Single();
        var compilation = await project.GetCompilationAsync();

        var diagnostics = await compilation!
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync();

        diagnostics.ContainsDiagnosticForFile("GirCore1001", "RaiseGirCore1001.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1002", "RaiseGirCore1002.cs");
        diagnostics.ContainsDiagnosticForFile("GirCore1003", "RaiseGirCore1003.cs");
    }
}
