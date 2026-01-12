using System.Linq;
using GObject.Integration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Gtk.Integration.SourceAnalyzer;

internal sealed class GirCore2001 : Rule
{
    public static SyntaxKind SyntaxKind => SyntaxKind.ClassDeclaration;

    public static DiagnosticDescriptor DiagnosticDescriptor { get; } = new(
        id: "GirCore2001",
        title: "GTK template class must be annotated with \"GObject.Subclass\" attribute.",
        messageFormat: "Adding a \"GObject.Subclass\" attribute at class level is required to create a GTK widget with a composite template support.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        helpLinkUri: DiagnosticLink.Create(2001)
    );

    public static void Analyze(SyntaxNodeAnalysisContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);

        if (classSymbol is null)
            return;

        if (!classSymbol.GetAttributes().Any(x => x.IsTemplateAttribute()))
            return;

        if (classSymbol.GetAttributes().Any(x => x.IsSubclassAttribute()))
            return;

        var diagnostic = Diagnostic.Create(DiagnosticDescriptor, classDeclarationSyntax.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
