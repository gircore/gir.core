using System;

namespace GirLoader.Output.Model
{
    internal class MemberFactory
    {
        public Member Create(Input.Model.Member member)
        {
            if (member.Name is null)
                throw new Exception("Member is missing name");

            if (member.Identifier is null)
                throw new Exception($"Member {member.Name} is missing an identifier");

            if (member.Value is null)
                throw new Exception($"Member {member.Name} is missing a value");

            var ident = Helper.String.ToPascalCase(member.Name);

            return new Member(
                elementName: new ElementName(member.Identifier),
                symbolName: new SymbolName(Helper.String.EscapeIdentifier(ident)),
                value: member.Value
            );
        }
    }
}
