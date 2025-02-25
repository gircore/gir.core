using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [
        GirCore1001.DiagnosticDescriptor,
        GirCore1002.DiagnosticDescriptor,
        GirCore1003.DiagnosticDescriptor
    ];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        RegisterDiagnosticRule<GirCore1001>(context);
        RegisterDiagnosticRule<GirCore1002>(context);
        RegisterDiagnosticRule<GirCore1003>(context);
    }

    private static void RegisterDiagnosticRule<T>(AnalysisContext context) where T : Rule
    {
        context.RegisterSyntaxNodeAction(T.Analyze, T.SyntaxKind);
    }
}
