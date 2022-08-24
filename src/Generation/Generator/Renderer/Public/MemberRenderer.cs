using Generator.Model;
using static System.FormattableString;

namespace Generator.Renderer.Public;

internal static class MemberRenderer
{
    public static string Render(GirModel.Member member)
        => Invariant($"{Member.GetName(member)} = {member.Value},");
}
