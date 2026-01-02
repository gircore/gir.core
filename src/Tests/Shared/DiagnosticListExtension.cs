using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.Tests;

public static class DiagnosticListExtension
{
    public static void ContainsDiagnostic(this IEnumerable<Diagnostic> diagnostics, string diagnosticId)
    {
        var diagnostic = diagnostics.FirstOrDefault(x => x.Id == diagnosticId);
        if (diagnostic is null)
            throw new Exception($"Diagnostic {diagnosticId} not raised");
    }

    public static void ContainsNoDiagnostic(this IEnumerable<Diagnostic> diagnostics, string diagnosticId)
    {
        var relevantDiagnostics = diagnostics.Where(x => x.Id == diagnosticId);
        if (relevantDiagnostics.Any())
            throw new Exception($"Diagnostic {diagnosticId} raised");
    }
}
