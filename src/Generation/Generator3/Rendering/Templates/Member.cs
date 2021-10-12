namespace Generator3.Rendering.Templates
{
    public static class Member
    {
        public static string Get(Generation.Model.Member member)
            => $"{ member.Name } = { member.Value },";
    }
}
