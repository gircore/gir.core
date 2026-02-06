using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1006 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ConstructorDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1006",
        title: "GObject subclass constructor with parameters must be replaced with a factory method.",
        messageFormat: "GObject does not use constructors to create instances. A subclass must support creation through the 'NewWithProperties' method which may not provide any parameters: If you use custom factory methods the code must be written in a way that supports parameterless creation of an instance. Implement a custom factory method to supply any optional members.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1006)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var constructorSyntax = (ConstructorDeclarationSyntax) context.Node;

        if (!constructorSyntax.ParameterList.Parameters.Any())
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
