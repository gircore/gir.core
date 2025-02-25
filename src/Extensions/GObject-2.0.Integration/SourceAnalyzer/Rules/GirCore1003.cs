using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1003 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ConstructorDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1003",
        title: "GObject subclass constructor must not call base constructor",
        messageFormat: "GObject subclass constructor must call a generated constructor like 'this()' instead of 'base()'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1003)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var constructorSyntax = (ConstructorDeclarationSyntax) context.Node;
        var initializerSyntax = constructorSyntax.Initializer;

        if (initializerSyntax is null || initializerSyntax.Kind() != SyntaxKind.BaseConstructorInitializer)
            return;

        if (constructorSyntax.Parent is not ClassDeclarationSyntax classSyntax)
            return;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classSyntax);
        if (classSymbol is null || !classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        var diagnostic = Diagnostic.Create(DiagnosticDescriptor, initializerSyntax.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
