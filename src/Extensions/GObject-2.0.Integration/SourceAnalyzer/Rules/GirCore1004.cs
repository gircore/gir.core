using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1004 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ClassDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1004",
        title: "GObject subclass may not be generic",
        messageFormat: "GObject subclass must be non-generic. GObject does not support generic types. Use a regular subclass (without SubclassAttribute) or extension blocks to allow type safe access to data.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1004)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        if (classDeclarationSyntax.Arity == 0)
            return;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
        if (classSymbol is null || !classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        var diagnostic = Diagnostic.Create(DiagnosticDescriptor, classDeclarationSyntax.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
