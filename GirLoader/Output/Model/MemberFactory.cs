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

            var name = Helper.String.EscapeIdentifier(member.Name);

            return new Member(
                originalName: new SymbolName(name),
                symbolName: new SymbolName(Helper.String.ToPascalCase(name)),
                value: member.Value
            );
        }
    }
}
