using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class AliasFactory 
    {
        public Symbol Create(AliasInfo aliasInfo)
        {
            if (aliasInfo.Type is null)
                throw new Exception("Alias is missing a type");
            
            if (aliasInfo.Name is null)
                throw new Exception("Alias is missing a name");

            if (aliasInfo.For?.Name is null)
                throw new Exception($"Alias {aliasInfo.Name} is missing target");

            return new Symbol(
                ctypeName: new CTypeName(aliasInfo.Type), 
                typeName: new TypeName(aliasInfo.Name), 
                nativeName: new NativeName(aliasInfo.For.Name),
                managedName: new ManagedName(aliasInfo.For.Name)
            );
        }
    }
}
