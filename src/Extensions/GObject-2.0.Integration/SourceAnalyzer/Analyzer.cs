using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [
        GirCore1002.DiagnosticDescriptor,
        GirCore1004.DiagnosticDescriptor,
        GirCore1005.DiagnosticDescriptor,
        GirCore1006.DiagnosticDescriptor
    ];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        RegisterDiagnosticRule<GirCore1002>(context);
        RegisterDiagnosticRule<GirCore1004>(context);
        RegisterDiagnosticRule<GirCore1005>(context);
        RegisterDiagnosticRule<GirCore1006>(context);
    }

    private static void RegisterDiagnosticRule<T>(AnalysisContext context) where T : Rule
    {
        context.RegisterSyntaxNodeAction(T.Analyze, T.SyntaxKind);
    }
}
