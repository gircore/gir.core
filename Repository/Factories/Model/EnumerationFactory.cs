using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class EnumerationFactory
    {
        private readonly MemberFactory _memberFactory;

        public EnumerationFactory(MemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }
        
        public Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            return new Enumeration(
                @namespace: @namespace,
                name: @enum.Name,
                managedName: @enum.Name,
                hasFlags: hasFlags,
                members: @enum.Members.Select(x => _memberFactory.Create(x)).ToList()
            );
        }
    }
}
