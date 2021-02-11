using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Class : Type
    {
        public string CType { get; }
        public ISymbolReference? Parent { get; }
        public IEnumerable<ISymbolReference> Implements { get; }
        
        public IEnumerable<Property> Properties { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        public IEnumerable<Field> Fields { get; }
        public IEnumerable<Signal> Signals { get; }
        public Method GetTypeFunction { get; }

        public Class(Namespace @namespace, string nativeName, string managedName, string ctype, ISymbolReference? parent, IEnumerable<ISymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties, IEnumerable<Field> fields, IEnumerable<Signal> signals) : base(@namespace, nativeName, managedName)
        {
            Parent = parent;
            Implements = implements;
            CType = ctype;
            Methods = methods;
            Functions = functions;
            GetTypeFunction = getTypeFunction;
            Properties = properties;
            Fields = fields;
            Signals = signals;
        }
    }
}
