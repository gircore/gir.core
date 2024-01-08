using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gir.Integration.CSharp.Gtk;

internal class CompositeTemplateSyntaxReceiver : ISyntaxReceiver
{
    public ClassDeclarationSyntax? TemplateClass { get; private set; }

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (NodeIsClass(syntaxNode, out var cds))
            if (ClassHasTemplateAttribute(cds))
                TemplateClass = cds;
    }

    private static bool NodeIsClass(SyntaxNode node, [NotNullWhen(true)] out ClassDeclarationSyntax? syntax)
    {
        if (node is ClassDeclarationSyntax cds)
            syntax = cds;
        else
            syntax = null;

        return syntax is not null;
    }

    private static bool ClassHasTemplateAttribute(ClassDeclarationSyntax cds)
    {
        var attributes = cds.AttributeLists.SelectMany(x => x.Attributes);
        return attributes.Any(x => x.Name.ToString() == "Template");
    }
}
