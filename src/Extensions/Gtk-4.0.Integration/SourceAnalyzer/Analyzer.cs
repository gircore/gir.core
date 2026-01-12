using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Gtk.Integration.SourceAnalyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [
        GirCore2001.DiagnosticDescriptor,
        GirCore2002.DiagnosticDescriptor
    ];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        RegisterDiagnosticRule<GirCore2001>(context);
        RegisterDiagnosticRule<GirCore2002>(context);
    }

    private static void RegisterDiagnosticRule<T>(AnalysisContext context) where T : Rule
    {
        context.RegisterSyntaxNodeAction(T.Analyze, T.SyntaxKind);
    }
}
