using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IMemberFactory
    {
        Member Create(MemberInfo info);
    }

    public class MemberFactory : IMemberFactory
    {
        private readonly IPascalCaseConverter _pascalCaseConverter;

        public MemberFactory(IPascalCaseConverter pascalCaseConverter)
        {
            _pascalCaseConverter = pascalCaseConverter;
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
                managedName: _pascalCaseConverter.Convert(info.Name), 
                value: info.Value
            );
        }
    }
}
