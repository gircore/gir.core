using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public class Class : ComplexType
    {
        #region Fields

        private IEnumerable<Method>? _methods;
        private IEnumerable<Method>? _functions;
        private IEnumerable<Method>? _constructors;
        private IEnumerable<Property>? _properties;
        private IEnumerable<Field>? _fields;
        private IEnumerable<Signal>? _signals;
        private IEnumerable<TypeReference>? _implements;

        #endregion

        #region Properties

        public Method GetTypeFunction { get; }
        public bool IsFundamental { get; init; }
        public TypeReference? Parent { get; init; }

        public IEnumerable<TypeReference> Implements
        {
            get => _implements ??= Enumerable.Empty<TypeReference>();
            init => _implements = value;
        }

        public IEnumerable<Method> Methods
        {
            get => _methods ??= Enumerable.Empty<Method>();
            init => _methods = value;
        }

        public IEnumerable<Method> Functions
        {
            get => _functions ??= Enumerable.Empty<Method>();
            init => _functions = value;
        }

        public IEnumerable<Property> Properties
        {
            get => _properties ??= Enumerable.Empty<Property>();
            init => _properties = value;
        }

        public IEnumerable<Field> Fields
        {
            get => _fields ??= Enumerable.Empty<Field>();
            init => _fields = value;
        }

        public IEnumerable<Signal> Signals
        {
            get => _signals ??= Enumerable.Empty<Signal>();
            init => _signals = value;
        }

        public IEnumerable<Method> Constructors
        {
            get => _constructors ??= Enumerable.Empty<Method>();
            init => _constructors = value;
        }

        #endregion

        public Class(Repository repository, CType? cType, TypeName originalName, TypeName name, Method getTypeFunction) : base(repository, cType, name, originalName)
        {
            GetTypeFunction = getTypeFunction;
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

            _methods = Methods.ToList().Where(IsResolved);
            _functions = Functions.ToList().Where(IsResolved);
            _constructors = Constructors.ToList().Where(IsResolved);
            _properties = Properties.ToList().Where(IsResolved);
            _signals = Signals.ToList().Where(IsResolved);
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

        private bool IsResolved(Symbol symbol)
        {
            var result = symbol.GetIsResolved();

            if (!result)
                Log.Information($"Class {Repository?.Namespace.Name}.{OriginalName}: Stripping symbol {symbol.OriginalName}");

            return result;
        }
    }
}
