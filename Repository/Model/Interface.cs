using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
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

        public Interface(Repository repository, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName, IEnumerable<TypeReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction, IEnumerable<Property> properties) : base(repository, cTypeName, typeName, symbolName)
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
                Log.Information($"Interface {Repository?.Namespace.Name}.{TypeName}: Stripping symbol {element.Name}");

            return !result;
        }
    }
}
