using System;
using System.Linq;

namespace GirLoader.Output
{
    internal class BitfieldFactory
    {
        private readonly MemberFactory _memberFactory;

        public BitfieldFactory(MemberFactory memberFactory)
        {
            _memberFactory = memberFactory;
        }

        public Bitfield Create(Input.Bitfield bitfield, Repository repository)
        {
            if (bitfield.Name is null)
                throw new Exception("Bitfield has no name");

            if (bitfield.Type is null)
                throw new Exception("Bitfield is missing a type");

            var members = bitfield.Members.Select(_memberFactory.Create).ToList();

            bool ValueIsULong(Member member)
            {
                // Bitfields are transfered as ulong and thus do not support negative values.
                // Negative values can occur as C convenience members as a "mask".
                // Those can safely be should be ignored.

                if (ulong.TryParse(member.Value, out _))
                    return true;

                Log.Verbose($"{nameof(Bitfield)} - {member.OriginalName} ignored because value is no ulong: {member.Value}");
                return false;
            }

            return new Bitfield(
                repository: repository,
                originalName: new TypeName(bitfield.Name),
                members: members.Where(ValueIsULong),
                cType: bitfield.Type
            );
        }
    }
}
