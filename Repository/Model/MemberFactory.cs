using System;
using Repository.Xml;

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

        public Member Create(MemberInfo info)
        {
            if (info.Name is null)
                throw new Exception("Member is missing name");

            if (info.Identifier is null)
                throw new Exception($"Member {info.Name} is missing an identifier");

            if (info.Value is null)
                throw new Exception($"Member {info.Name} is missing a value");

            var ident = _caseConverter.ToPascalCase(info.Name);

            return new Member(
                elementName: new ElementName(info.Identifier),
                symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(ident)),
                value: info.Value
            );
        }
    }
}
