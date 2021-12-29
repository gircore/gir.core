using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Class : ComplexType
    {
        private readonly List<Method> _methods;
        private readonly List<Function> _functions;
        private readonly List<Constructor> _constructors;
        private readonly List<Property> _properties;
        private readonly List<Field> _fields;
        private readonly List<Signal> _signals;
        public bool IsFundamental { get; }
        public Function GetTypeFunction { get; }
        public IEnumerable<TypeReference> Implements { get; }
        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Function> Functions => _functions;
        public TypeReference? Parent { get; }
        public IEnumerable<Property> Properties => _properties;
        public IEnumerable<Field> Fields => _fields;
        public IEnumerable<Signal> Signals => _signals;
        public IEnumerable<Constructor> Constructors => _constructors;
        public bool Introspectable { get; }

        public Class(Repository repository, string? cType, string name, TypeReference? parent, IEnumerable<TypeReference> implements, IEnumerable<Method> methods, IEnumerable<Function> functions, Function getTypeFunction, IEnumerable<Property> properties, IEnumerable<Field> fields, IEnumerable<Signal> signals, IEnumerable<Constructor> constructors, bool isFundamental, bool introspectable) : base(repository, cType, name)
        {
            Parent = parent;
            Implements = implements;
            GetTypeFunction = getTypeFunction;
            Introspectable = introspectable;

            this._methods = methods.ToList();
            this._functions = functions.ToList();
            this._constructors = constructors.ToList();
            this._properties = properties.ToList();
            this._fields = fields.ToList();
            this._signals = signals.ToList();

            IsFundamental = isFundamental;
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is not null && typeReference.CTypeReference.CType != "gpointer")
                return typeReference.CTypeReference.CType == CType;

            if (typeReference.SymbolNameReference is not null)
            {
                var nameMatches = typeReference.SymbolNameReference.SymbolName == Name;
                var namespaceMatches = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name;
                var namespaceMissing = typeReference.SymbolNameReference.NamespaceName == null;

                return nameMatches && (namespaceMatches || namespaceMissing);
            }

            return false;
        }
    }
}
