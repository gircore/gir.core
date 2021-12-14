using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Union : ComplexType
    {
        private readonly List<Method> _methods;
        private readonly List<Function> _functions;
        private readonly List<Constructor> _constructors;
        private readonly List<Field> _fields;

        public Function? GetTypeFunction { get; }
        public IEnumerable<Field> Fields => _fields;
        public bool Disguised { get; }
        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Constructor> Constructors => _constructors;
        public IEnumerable<Function> Functions => _functions;

        public Union(Repository repository, string? cType, string name, IEnumerable<Method> methods, IEnumerable<Function> functions, Function? getTypeFunction, IEnumerable<Field> fields, bool disguised, IEnumerable<Constructor> constructors) : base(repository, cType, name)
        {
            GetTypeFunction = getTypeFunction;
            Disguised = disguised;

            this._constructors = constructors.ToList();
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            this._fields = fields.ToList();
        }

        internal override bool Matches(TypeReference typeReference)
        {
            return typeReference switch
            {
                { CTypeReference: { } cr } => cr.CType == CType,
                { SymbolNameReference: { } sr } => sr.SymbolName == Name,
                _ => throw new Exception($"Can't match {nameof(Union)} with {nameof(TypeReference)} {typeReference}")
            };
        }
    }
}
