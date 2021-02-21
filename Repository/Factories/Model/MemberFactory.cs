using System;
using System.Linq;
using System.Text.RegularExpressions;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class MemberFactory
    {
        private readonly CaseConverter _caseConverter;

        public MemberFactory(CaseConverter caseConverter)
        {
            _caseConverter = caseConverter;
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
                nativeName: info.Identifier, 
                managedName: Validation.EscapeIdentifier(ident),
                value: info.Value
            );
        }
    }
}
