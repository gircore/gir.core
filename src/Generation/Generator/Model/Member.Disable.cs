using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Member
{
    private static readonly HashSet<GirModel.Member> DisabledMembers = new();

    public static void Disable(GirModel.Member member)
    {
        DisabledMembers.Add(member);
    }

    public static bool IsEnabled(GirModel.Member member)
    {
        return !DisabledMembers.Contains(member);
    }
}
