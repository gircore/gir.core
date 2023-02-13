using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Member
{
    private static readonly HashSet<GirModel.Member> DisabledMembers = new();

    public static void Disable(GirModel.Member member)
    {
        lock (DisabledMembers)
        {
            DisabledMembers.Add(member);
        }
    }

    public static bool IsEnabled(GirModel.Member member)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !DisabledMembers.Contains(member);
    }
}
