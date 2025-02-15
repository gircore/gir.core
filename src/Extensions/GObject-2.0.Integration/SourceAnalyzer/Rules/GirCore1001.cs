using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GObject.Integration.SourceAnalyzer;

internal sealed class GirCore1001 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ConstructorDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore1001",
        title: "GObject subclass constructor with parameters must call 'this()' constructor",
        messageFormat: "GObject subclass constructor with parameters must call a generated constructor like 'this()'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(1001)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var constructorSyntax = (ConstructorDeclarationSyntax) context.Node;

        if (!constructorSyntax.ParameterList.Parameters.Any())
            return;

        var initializerSyntax = constructorSyntax.Initializer;

        if (initializerSyntax is not null)
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
