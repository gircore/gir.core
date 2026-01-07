using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1005 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ClassDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1005",
        title: "GObject subclass may not be nested inside a generic type",
        messageFormat: "GObject subclass must not be declared inside a generic type. Use a regular subclass (without SubclassAttribute) or extension blocks to allow type safe access to data.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1005)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
        if (classSymbol is null || !classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        TypeDeclarationSyntax? syntax = classDeclarationSyntax.Parent as ClassDeclarationSyntax;
        while (syntax is not null)
        {
            if (syntax.Arity > 0)
            {
                var diagnostic = Diagnostic.Create(DiagnosticDescriptor, classDeclarationSyntax.GetLocation());
                context.ReportDiagnostic(diagnostic);
                return;
            }

            syntax = syntax.Parent as ClassDeclarationSyntax;
        }

    }
}
