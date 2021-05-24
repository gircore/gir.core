using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output.Model
{
    public class Interface : Type
    {
        private readonly List<Method> _methods;
        private readonly List<Method> _functions;
        private readonly List<Property> _properties;

        public Method GetTypeFunction { get; }
        public IEnumerable<TypeReference> Implements { get; }

        public IEnumerable<Method> Methods => _methods;
        public IEnumerable<Method> Functions => _functions;
        public IEnumerable<Property> Properties => _properties;

        public Interface(Repository repository, CType? cType, SymbolName originalName, SymbolName symbolName, IEnumerable<TypeReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties) : base(repository, cType, originalName, symbolName)
        {
            Implements = implements;
            this._methods = methods.ToList();
            this._functions = functions.ToList();
            GetTypeFunction = getTypeFunction;
            _properties = properties.ToList();
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            return IEnumerables.Concat(
                Implements,
                GetTypeFunction.GetTypeReferences(),
                Methods.GetTypeReferences(),
                Functions.GetTypeReferences()
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

        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();

            if (!result)
                Log.Information($"Interface {Repository?.Namespace.Name}.{OriginalName}: Stripping symbol {symbol.OriginalName}");

            return !result;
        }
    }
}
