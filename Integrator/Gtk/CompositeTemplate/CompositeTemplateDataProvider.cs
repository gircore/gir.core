using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Integrator.Gtk
{
    public class CompositeTemplateDataProvider
    {
        private readonly ClassDeclarationSyntax _classDeclarationSyntax;

        public string ClassName => GetClassName();
        public string TemplateName => GetTemplateName();
        
        public CompositeTemplateDataProvider(ClassDeclarationSyntax classDeclarationSyntax)
        {
            _classDeclarationSyntax = classDeclarationSyntax ?? throw new ArgumentNullException(nameof(classDeclarationSyntax));
        }

        private string GetClassName()
        {
            return _classDeclarationSyntax.Identifier.ValueText;
        }

        private string GetTemplateName()
        {
            var attributes = _classDeclarationSyntax.AttributeLists.SelectMany(x => x.Attributes);
            var attribute = attributes.First(x => x.Name.ToString() == "Template");

            return attribute.ArgumentList.Arguments.First().ToString();
        }
    }
}
