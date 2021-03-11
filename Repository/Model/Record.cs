using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Record : Symbol
    {
        private readonly List<Method> _methods;
        private readonly List<Method> _functions;
        private readonly List<Method> _constructors;
        private readonly List<Field> _fields;

        public RecordType Type => this switch
        {
            {Disguised: true, GLibClassStructFor: { }} => RecordType.PrivateClass,
            {Disguised: false, GLibClassStructFor: { }} => RecordType.PublicClass,
            {Disguised: false, Fields: { } f} when f.Any() => RecordType.Value,

            //As structured types are always passed around via pointers
            //they need to be Ref types if there are any methods or constructors
            {Constructors: { } c} when c.Any() => RecordType.Ref,
            {Methods : { } m} when m.Any() => RecordType.Ref,

            _ => RecordType.Opaque
        };

        public Method? GetTypeFunction { get; }
        public IEnumerable<Field> Fields => _fields;
        public bool Disguised { get; }
        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Method> Constructors => _constructors;
        public IEnumerable<Method> Functions => _functions;
        public SymbolReference? GLibClassStructFor { get; }

        public Record(Namespace @namespace, string name, string managedName, SymbolReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Method> functions, Method? getTypeFunction, IEnumerable<Field> fields, bool disguised, IEnumerable<Method> constructors, string? ctype) : base(@namespace, ctype, name, managedName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
            GetTypeFunction = getTypeFunction;
            Disguised = disguised;
            
            this._constructors = constructors.ToList();
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            this._fields = fields.ToList();
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                Constructors.GetSymbolReferences(),
                Fields.GetSymbolReferences(),
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences()
            );

            if (GetTypeFunction is { })
                symbolReferences = symbolReferences.Concat(GetTypeFunction.GetSymbolReferences());

            if (GLibClassStructFor is { })
                symbolReferences = symbolReferences.Append(GLibClassStructFor);

            return symbolReferences;
        }

        internal override bool GetIsResolved()
        {
            if (!(GetTypeFunction?.GetIsResolved() ?? true))
                return false;

            return Methods.AllResolved()
                   && Functions.AllResolved()
                   && Constructors.AllResolved()
                   && Fields.AllResolved();
        }
        
        internal override void Strip()
        {
            //Fields are not cleaned as those are needed
            //to represent the native structure of the object / class
            
            _methods.RemoveAll(Remove);
            _functions.RemoveAll(Remove);
            _constructors.RemoveAll(Remove);
        }
        
        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();
            
            if(!result)
                Log.Information($"Removing record symbol {symbol?.Namespace?.Name} {symbol?.Name} because it is not resolved");

            return !result;
        }
    }
}
