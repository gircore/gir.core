using System;
using System.Linq;

namespace GirLoader.Output
{
    internal class EnumerationFactory
    {
        private readonly MemberFactory _memberFactory;

        public EnumerationFactory(MemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }

        public Enumeration Create(Input.Enum @enum, Repository repository)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            if (@enum.Type is null)
                throw new Exception("Enum is missing a type");

            return new Enumeration(
                repository: repository,
                originalName: new TypeName(@enum.Name),
                name: new TypeName(@enum.Name),
                members: @enum.Members.Select(_memberFactory.Create).ToList(),
                cType:@enum.Type
            );
        }
    }
}
