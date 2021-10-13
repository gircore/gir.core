namespace Generator3.Renderer
{
    public static class Member
    {
        public static string Get(Model.Member member)
            => $"{ member.Name } = { member.Value },";
    }
}
