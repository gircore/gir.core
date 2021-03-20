using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class AliasFactory 
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;

        public AliasFactory(SymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }
        
        public Alias Create(AliasInfo aliasInfo, Namespace @namespace)
        {
            if (aliasInfo.Type is null)
                throw new Exception("Alias is missing a type");
            
            if (aliasInfo.Name is null)
                throw new Exception("Alias is missing a name");

            if (aliasInfo.For?.Name is null)
                throw new Exception($"Alias {aliasInfo.Name} is missing target");

            return new Alias(
                elementName: new ElementName(aliasInfo.Type),
                symbolName: new SymbolName(aliasInfo.Name),
                symbolReference: _symbolReferenceFactory.Create(aliasInfo.For, @namespace.Name),
                @namespace: @namespace
            );
        }
    }
}
