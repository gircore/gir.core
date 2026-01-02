using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Gtk.Integration.SourceAnalyzer;

internal interface Rule
{
    static abstract SyntaxKind SyntaxKind { get; }
    static abstract void Analyze(SyntaxNodeAnalysisContext context);
}
