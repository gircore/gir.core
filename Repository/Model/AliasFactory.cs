using System;

namespace Repository.Model
{
    internal class AliasFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;

        public AliasFactory(SymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }

        public Alias Create(Xml.Alias alias, Namespace @namespace)
        {
            if (alias.Type is null)
                throw new Exception("Alias is missing a type");

            if (alias.Name is null)
                throw new Exception("Alias is missing a name");

            if (alias.For?.Name is null)
                throw new Exception($"Alias {alias.Name} is missing target");

            return new Alias(
                elementName: new ElementName(alias.Type),
                symbolName: new SymbolName(alias.Name),
                symbolReference: _symbolReferenceFactory.Create(alias.For, @namespace.Name),
                @namespace: @namespace
            );
        }
    }
}
