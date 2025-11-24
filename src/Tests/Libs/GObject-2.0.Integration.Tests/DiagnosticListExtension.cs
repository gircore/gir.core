using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.Tests;

public static class DiagnosticListExtension
{

    public static void ContainsDiagnosticForFile(this IEnumerable<Diagnostic> diagnostics, string diagnosticId, string expectedFile)
    {
        var diagnostic = diagnostics.FirstOrDefault(x => x.Id == diagnosticId);
        if (diagnostic is null)
            throw new Exception($"Diagnostic {diagnosticId} not raised");

        var sourceFile = System.IO.Path.GetFileName(diagnostic.Location.SourceTree!.FilePath);
        if (sourceFile != expectedFile)
            throw new Exception($"Diagnostic {diagnosticId} not raised in {expectedFile}");
    }

    public static void ContainsNoDiagnosticForFile(this IEnumerable<Diagnostic> diagnostics, string diagnosticId, string expectedFile)
    {
        var relevantDiagnostics = diagnostics.Where(x => x.Id == diagnosticId);
        var containedInFile = relevantDiagnostics.Any(x => x.Location.SourceTree!.FilePath == expectedFile);
        if (containedInFile)
            throw new Exception($"Diagnostic {diagnosticId} raised in {expectedFile}");
    }
}
