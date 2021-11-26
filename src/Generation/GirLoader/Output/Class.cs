using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

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

        public Class(Repository repository, CType? cType, TypeName originalName, TypeName name, TypeReference? parent, IEnumerable<TypeReference> implements, IEnumerable<Method> methods, IEnumerable<Function> functions, Function getTypeFunction, IEnumerable<Property> properties, IEnumerable<Field> fields, IEnumerable<Signal> signals, IEnumerable<Constructor> constructors, bool isFundamental) : base(repository, cType, name, originalName)
        {
            Parent = parent;
            Implements = implements;
            GetTypeFunction = getTypeFunction;

            this._methods = methods.ToList();
            this._functions = functions.ToList();
            this._constructors = constructors.ToList();
            this._properties = properties.ToList();
            this._fields = fields.ToList();
            this._signals = signals.ToList();

            IsFundamental = isFundamental;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                Implements,
                GetTypeFunction.GetTypeReferences(),
                Constructors.SelectMany(x => x.GetTypeReferences()),
                Methods.SelectMany(x => x.GetTypeReferences()),
                Functions.SelectMany(x => x.GetTypeReferences()),
                Properties.SelectMany(x => x.GetTypeReferences()),
                Fields.SelectMany(x => x.GetTypeReferences()),
                Signals.SelectMany(x => x.GetTypeReferences())
            );

            if (Parent is { })
                symbolReferences = symbolReferences.Append(Parent);

            return symbolReferences;
        }

        internal override bool GetIsResolved()
        {
            if (Parent is { } && !Parent.GetIsResolved())
                return false;

            if (!Implements.All(x => x.GetIsResolved()))
                return false;

            if (!GetTypeFunction.GetIsResolved())
                return false;

            return Methods.All(x => x.GetIsResolved())
                   && Functions.All(x => x.GetIsResolved())
                   && Constructors.All(x => x.GetIsResolved())
                   && Properties.All(x => x.GetIsResolved())
                   && Fields.All(x => x.GetIsResolved())
                   && Signals.All(x => x.GetIsResolved());
        }

        internal override void Strip()
        {
            //Fields are not cleaned as those are needed
            //to represent the native structure of the object / class

            _methods.RemoveAll(Remove);
            _functions.RemoveAll(Remove);
            _constructors.RemoveAll(Remove);
            _properties.RemoveAll(Remove);
            _signals.RemoveAll(Remove);
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

        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();

            if (!result)
                Log.Information($"Class {Repository?.Namespace.Name}.{OriginalName}: Stripping symbol {symbol.OriginalName}");

            return !result;
        }
    }
}
