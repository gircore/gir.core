using static System.FormattableString;

namespace Generator3.Renderer.Public
{
    public static class Member
    {
        public static string Render(this Model.Public.Member member)
            => Invariant($"{ member.Name } = { member.Value },");
    }
}
