using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1002 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ConstructorDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1002",
        title: "GObject subclass constructor without parameters must use partial 'Initialize' method",
        messageFormat: "GObject subclass constructor without parameters must use 'partial void Initialize()' method instead of a constructor",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1002)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var constructorSyntax = (ConstructorDeclarationSyntax) context.Node;

        if (constructorSyntax.Modifiers.Any(SyntaxKind.StaticKeyword))
            return;

        if (constructorSyntax.ParameterList.Parameters.Any())
            return;

        if (constructorSyntax.Parent is not ClassDeclarationSyntax classSyntax)
            return;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classSyntax);
        if (classSymbol is null || !classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        var diagnostic = Diagnostic.Create(DiagnosticDescriptor, constructorSyntax.Identifier.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
