using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Integrator.Gtk
{
    public class CompositeTemplateDataProvider
    {
        private readonly ClassDeclarationSyntax _classDeclarationSyntax;

        #region Properties
        
        public string ClassName => GetClassName();
        public string TemplateName => GetTemplateName();
        
        #endregion
        
        #region Constructors
        
        public CompositeTemplateDataProvider(ClassDeclarationSyntax classDeclarationSyntax)
        {
            _classDeclarationSyntax = classDeclarationSyntax ?? throw new ArgumentNullException(nameof(classDeclarationSyntax));
        }

        #endregion
        
        #region Methods
        
        private string GetClassName()
        {
            return _classDeclarationSyntax.Identifier.ValueText;
        }

        private string GetTemplateName()
        {
            var attribute = GetClassAttribute("Template");
            var firstArgument = GetFirstArgument(attribute);
            
            return firstArgument;
        }

        private AttributeSyntax GetClassAttribute(string attributeName)
        {
            var attributes = _classDeclarationSyntax.AttributeLists.SelectMany(x => x.Attributes);
            var attribute = attributes.First(x => x.Name.ToString() == attributeName);

            return attribute;
        }
        
        private string GetFirstArgument(AttributeSyntax attribute)
        {
            if (attribute.ArgumentList is null)
                throw new Exception($"Class {ClassName} does not contain a \"Template\" attribute with arguments.");
            
            return attribute.ArgumentList.Arguments.First().ToString();
        }
        
        #endregion
    }
}
