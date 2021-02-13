using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Class : Interface
    {
        public ISymbolReference? Parent { get; }
        public IEnumerable<Property> Properties { get; }
        public IEnumerable<Field> Fields { get; }
        public IEnumerable<Signal> Signals { get; }

        public Class(Namespace @namespace, string nativeName, string managedName, string ctype, ISymbolReference? parent, IEnumerable<ISymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties, IEnumerable<Field> fields, IEnumerable<Signal> signals) : base(@namespace, nativeName, managedName, ctype, implements, methods, functions, getTypeFunction)
        {
            Parent = parent;
            Properties = properties;
            Fields = fields;
            Signals = signals;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                base.GetSymbolReferences(),
                Properties.GetSymbolReferences(),
                Fields.GetSymbolReferences(),
                Signals.GetSymbolReferences()
            );

            if (Parent is { })
                symbolReferences = symbolReferences.Append(Parent);

            return symbolReferences;
        }
    }
}
