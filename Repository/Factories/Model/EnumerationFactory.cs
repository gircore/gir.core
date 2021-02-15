using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IEnumartionFactory
    {
        Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags);
    }

    public class EnumerationFactory : IEnumartionFactory
    {
        private readonly IMemberFactory _memberFactory;

        public EnumerationFactory(IMemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }
        
        public Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            return new Enumeration(
                @namespace: @namespace,
                nativeName: @enum.Name,
                managedName: @enum.Name,
                hasFlags: hasFlags,
                members: @enum.Members.Select(x => _memberFactory.Create(x)).ToList()
            );
        }
    }
}
