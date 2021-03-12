using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Class : Symbol
    {
        private readonly List<Method> _methods;
        private readonly List<Method> _functions;
        private readonly List<Method> _constructors;
        private readonly List<Property> _properties;
        private readonly List<Field> _fields;
        private readonly List<Signal> _signals;
        public bool IsFundamental { get; }
        public string? CType { get; }
        public Method GetTypeFunction { get; }
        public IEnumerable<SymbolReference> Implements { get; }
        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Method> Functions => _functions;
        public SymbolReference? Parent { get; }
        public IEnumerable<Property> Properties => _properties;
        public IEnumerable<Field> Fields => _fields;
        public IEnumerable<Signal> Signals => _signals;
        public IEnumerable<Method> Constructors => _constructors;

        public Class(Namespace @namespace, string name, string managedName, string? ctype, SymbolReference? parent, IEnumerable<SymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties, IEnumerable<Field> fields, IEnumerable<Signal> signals, IEnumerable<Method> constructors, bool isFundamental) : base(@namespace, ctype, name, managedName, managedName)
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
            CType = ctype;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                Implements,
                GetTypeFunction.GetSymbolReferences(),
                Constructors.GetSymbolReferences(),
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences(),
                Properties.GetSymbolReferences(),
                Fields.GetSymbolReferences(),
                Signals.GetSymbolReferences()
            );

            if (Parent is { })
                symbolReferences = symbolReferences.Append(Parent);

            return symbolReferences;
        }

        internal override bool GetIsResolved()
        {
            if (Parent is { } && !Parent.IsResolved)
                return false;
            
            if(!Implements.AllResolved())
                return false;

            if (!GetTypeFunction.GetIsResolved())
                return false;

            return Methods.AllResolved() 
                   && Functions.AllResolved() 
                   && Constructors.AllResolved() 
                   && Properties.AllResolved() 
                   && Fields.AllResolved() 
                   && Signals.AllResolved();
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
        
        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();
            
            if(!result)
                Log.Information($"Class {Namespace?.Name}.{Name}: Stripping symbol {symbol?.Name}");

            return !result;
        }
    }
}
