using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Fixer;

public static class Bitfields
{
    public static void Fixup(IEnumerable<GirModel.Bitfield> bitfields)
    {
        foreach (var bitfield in bitfields)
            Fixup(bitfield);
    }

    private static void Fixup(GirModel.Bitfield bitfield)
    {
        DisableDuplicateMembers(bitfield);
    }

    private static void DisableDuplicateMembers(GirModel.Bitfield bitfield)
    {
        foreach (var grouping in bitfield.Members.GroupBy(member => member.Name))
        {
            if (grouping.Count() <= 1)
                continue;

            foreach (var member in grouping.Skip(1)) //Disable all but the first member
            {
                Member.Disable(member);
                Log.Debug($"{bitfield.Name}: Disabled member {member.Name} with value {member.Value} because there is another member with the same name");
            }
        }
    }
}
