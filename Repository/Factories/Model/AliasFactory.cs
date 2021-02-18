using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class AliasFactory 
    {
        public Symbol Create(AliasInfo aliasInfo)
        {
            if (aliasInfo.Name is null)
                throw new Exception("Alias is missing name");

            if (aliasInfo.For?.Name is null)
                throw new Exception($"Alias {aliasInfo.Name} is missing target");

            return new BasicSymbol(aliasInfo.Name, aliasInfo.For.Name);
        }
    }
}
