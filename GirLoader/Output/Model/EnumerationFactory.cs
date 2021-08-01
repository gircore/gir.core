using System;
using System.Linq;

namespace GirLoader.Output.Model
{
    internal class EnumerationFactory
    {
        private readonly MemberFactory _memberFactory;

        public EnumerationFactory(MemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }

        public Enumeration Create(Input.Model.Enum @enum, Repository repository, bool hasFlags)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            if (@enum.Type is null)
                throw new Exception("Enum is missing a type");

            return new Enumeration(
                repository: repository,
                originalName: new TypeName(@enum.Name),
                name: new TypeName(@enum.Name),
                hasFlags: hasFlags,
                members: @enum.Members.Select(_memberFactory.Create).ToList(),
                cType: new CType(@enum.Type)
            );
        }
    }
}
