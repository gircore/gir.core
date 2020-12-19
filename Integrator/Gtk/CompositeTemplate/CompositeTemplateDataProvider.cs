using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Integrator.Gtk
{
    public class CompositeTemplateDataProvider
    {
        #region Fields
        
        private readonly ClassDeclarationSyntax _classDeclarationSyntax;

        #endregion
        
        #region Properties
        
        public string ClassName => GetClassName();
        public string TemplateName => GetTemplateName();
        public IEnumerable<string> ConnectFields => GetConnectFields();

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
            var attributeSyntax = SyntaxHelper.GetAttributeSyntax(_classDeclarationSyntax, "Template");
            var firstArgument = SyntaxHelper.GetFirstArgument(attributeSyntax);
            
            return firstArgument;
        }

        private IEnumerable<string> GetConnectFields()
        {
            var fieldSyntaxes = SyntaxHelper.GetFieldSyntaxes(_classDeclarationSyntax);
            var fields = GetConnectFields(fieldSyntaxes);
            
            return fields;
        }
 
        private IEnumerable<string> GetConnectFields(IEnumerable<FieldDeclarationSyntax> fieldSyntaxes)
        {
            foreach (var fieldSyntax in fieldSyntaxes)
            {
                if (SyntaxHelper.HasAttribute(fieldSyntax, "Connect"))
                {
                    yield return SyntaxHelper.GetFirstFieldName(fieldSyntax);
                }
            }
        }

        #endregion
    }
}
