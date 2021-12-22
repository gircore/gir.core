using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Record : ComplexType
    {
        private readonly List<Method> _methods;
        private readonly List<Function> _functions;
        private readonly List<Constructor> _constructors;
        private readonly List<Field> _fields;

        public bool IsClassStruct => GLibClassStructFor is { };

        public Function? GetTypeFunction { get; }
        public IEnumerable<Field> Fields => _fields;
        public bool Disguised { get; }
        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Constructor> Constructors => _constructors;
        public IEnumerable<Function> Functions => _functions;
        public TypeReference? GLibClassStructFor { get; }

        public Record(Repository repository, string? cType, string name, TypeReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Function> functions, Function? getTypeFunction, IEnumerable<Field> fields, bool disguised, IEnumerable<Constructor> constructors) : base(repository, cType, name)
        {
            GLibClassStructFor = gLibClassStructFor;
            GetTypeFunction = getTypeFunction;
            Disguised = disguised;

            this._constructors = constructors.ToList();
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            this._fields = fields.ToList();
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
