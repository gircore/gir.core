using System;

namespace Gir.Model
{
    internal class MemberFactory
    {
        public Member Create(Xml.Member member)
        {
            if (member.Name is null)
                throw new Exception("Member is missing name");

            if (member.Identifier is null)
                throw new Exception($"Member {member.Name} is missing an identifier");

            if (member.Value is null)
                throw new Exception($"Member {member.Name} is missing a value");

            var ident = CaseConverter.ToPascalCase(member.Name);

            return new Member(
                elementName: new ElementName(member.Identifier),
                symbolName: new SymbolName(IdentifierConverter.EscapeIdentifier(ident)),
                value: member.Value
            );
        }
    }
}
