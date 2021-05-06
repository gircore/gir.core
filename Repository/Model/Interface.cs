using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public class Interface : Symbol
    {
        private readonly List<Method> _methods;
        private readonly List<Method> _functions;
        private readonly List<Property> _properties;

        public Method GetTypeFunction { get; }
        public IEnumerable<SymbolReference> Implements { get; }

        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Method> Functions => _functions;
        public IEnumerable<Property> Properties => _properties;

        public Interface(Namespace @namespace, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName, IEnumerable<SymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties) : base(@namespace, cTypeName, typeName, symbolName)
        {
            Implements = implements;
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            GetTypeFunction = getTypeFunction;
            _properties = properties.ToList();
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

        public override bool GetIsResolved()
        {
            if (!Implements.AllResolved())
                return false;

            if (!GetTypeFunction.GetIsResolved())
                return false;

            return Methods.AllResolved()
                   && Functions.AllResolved();
        }

        private bool Remove(Element element)
        {
            var result = element.GetIsResolved();

            if (!result)
                Log.Information($"Interface {Namespace?.Name}.{TypeName}: Stripping symbol {element.Name}");

            return !result;
        }
    }
}
