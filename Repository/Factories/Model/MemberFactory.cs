using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public class MemberFactory
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
            
            return new Member(
                nativeName: info.Identifier, 
                managedName: _caseConverter.ToPascalCase(info.Name), 
                value: info.Value
            );
        }
    }
}
