using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gir.Integration.CSharp.Gtk
{
    internal class CompositeTemplateDataProvider
    {
        #region Fields
        
        private readonly ClassDeclarationSyntax _classDeclarationSyntax;
        private readonly Compilation _compilation;

        #endregion
        
        #region Properties

        public string Namespace => GetNamespace();
        public string ClassName => GetClassName();
        public string TemplateName => GetTemplateName();
        public IEnumerable<string> ConnectFields => GetConnectFields();

        #endregion
        
        #region Constructors
        
        public CompositeTemplateDataProvider(ClassDeclarationSyntax classDeclarationSyntax, Compilation compilation)
        {
            _classDeclarationSyntax = classDeclarationSyntax ?? throw new ArgumentNullException(nameof(classDeclarationSyntax));
            _compilation = compilation ?? throw new ArgumentNullException(nameof(compilation));
        }

        #endregion
        
        #region Methods
        
        private string GetNamespace()
        {
            var model = _compilation.GetSemanticModel(_classDeclarationSyntax.SyntaxTree, true);
            var type = model.GetDeclaredSymbol(_classDeclarationSyntax);

            if (type?.ContainingNamespace is null)
                throw new Exception("Could not determine namespace for class");

            return type.ContainingNamespace.Name;
        }

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
