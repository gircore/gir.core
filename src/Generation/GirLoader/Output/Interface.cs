using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Interface : ComplexType
    {
        private readonly List<Method> _methods;
        private readonly List<Function> _functions;
        private readonly List<Property> _properties;

        public Function GetTypeFunction { get; }
        public IEnumerable<TypeReference> Implements { get; }

        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Function> Functions => _functions;
        public IEnumerable<Property> Properties => _properties;

        public Interface(Repository repository, string? cType, string originalName, IEnumerable<TypeReference> implements, IEnumerable<Method> methods, IEnumerable<Function> functions, Function getTypeFunction, IEnumerable<Property> properties) : base(repository, cType, originalName)
        {
            Implements = implements;
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            GetTypeFunction = getTypeFunction;
            _properties = properties.ToList();
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is not null && typeReference.CTypeReference.CType != "gpointer")
                return typeReference.CTypeReference.CType == CType;

            if (typeReference.SymbolNameReference is not null)
            {
                var nameMatches = typeReference.SymbolNameReference.SymbolName == OriginalName;
                var namespaceMatches = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name;
                var namespaceMissing = typeReference.SymbolNameReference.NamespaceName == null;

                return nameMatches && (namespaceMatches || namespaceMissing);
            }

            return false;
        }
    }
}
