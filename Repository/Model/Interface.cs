using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Interface : Symbol
    {
        private readonly List<Method> _methods;
        private readonly List<Method> _functions;
        
        public string CType { get; }
        public Method GetTypeFunction { get; }
        public IEnumerable<SymbolReference> Implements { get; }

        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Method> Functions => _functions;
        
        public Interface(Namespace @namespace, string name, string managedName, string cType, IEnumerable<SymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction) : base(@namespace, cType, name, managedName, managedName)
        {
            CType = cType;
            Implements = implements;
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            GetTypeFunction = getTypeFunction;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                Implements,
                GetTypeFunction.GetSymbolReferences(),
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences()
            );
        }
        
        internal override void Strip()
        {
            _methods.RemoveAll(Remove);
            _functions.RemoveAll(Remove);
        }
        
        internal override bool GetIsResolved()
        {
            if(!Implements.AllResolved())
                return false;

            if (!GetTypeFunction.GetIsResolved())
                return false;

            return Methods.AllResolved()
                   && Functions.AllResolved();
        }
        
        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();
            
            if(!result)
                Log.Information($"Interface {Namespace?.Name}.{Name}: Stripping symbol {symbol?.Name}");

            return !result;
        }
    }
}
