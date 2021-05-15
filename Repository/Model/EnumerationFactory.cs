using System;
using System.Linq;

namespace Repository.Model
{
    internal class EnumerationFactory
    {
        private readonly MemberFactory _memberFactory;

        public EnumerationFactory(MemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }

        public Enumeration Create(Xml.Enum @enum, Repository repository, bool hasFlags)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            if (@enum.Type is null)
                throw new Exception("Enum is missing a type");

            return new Enumeration(
                repository: repository,
                typeName: new TypeName(@enum.Name),
                symbolName: new SymbolName(@enum.Name),
                hasFlags: hasFlags,
                members: @enum.Members.Select(x => _memberFactory.Create(x)).ToList(),
                cTypeName: new CTypeName(@enum.Type)
            );
        }
    }
}
