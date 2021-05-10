using System;

namespace Repository.Model
{
    internal class MemberFactory
    {
        private readonly CaseConverter _caseConverter;
        private readonly IdentifierConverter _identifierConverter;

        public MemberFactory(CaseConverter caseConverter, IdentifierConverter identifierConverter)
        {
            _caseConverter = caseConverter;
            _identifierConverter = identifierConverter;
        }

        public Member Create(Xml.Member member)
        {
            if (member.Name is null)
                throw new Exception("Member is missing name");

            if (member.Identifier is null)
                throw new Exception($"Member {member.Name} is missing an identifier");

            if (member.Value is null)
                throw new Exception($"Member {member.Name} is missing a value");

            var ident = _caseConverter.ToPascalCase(member.Name);

            return new Member(
                elementName: new ElementName(member.Identifier),
                symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(ident)),
                value: member.Value
            );
        }
    }
}
