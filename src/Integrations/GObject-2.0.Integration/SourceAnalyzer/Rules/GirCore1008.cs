using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1008 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ClassDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1008",
        title: "GObject subclass can not be private.",
        messageFormat: "To register GObject subclasses with the GObject type system, they must be accessible inside the assembly. 'public' and 'internal' access modifiers are supported.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1008)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
        if (classSymbol is null || !classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        if (classSymbol.DeclaredAccessibility != Accessibility.Private)
            return;

        var diagnostic = Diagnostic.Create(DiagnosticDescriptor, classDeclarationSyntax.Modifiers.First(x => x.Text == "private").GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
