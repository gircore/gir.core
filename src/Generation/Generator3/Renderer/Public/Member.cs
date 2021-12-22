namespace Generator3.Renderer.Public
{
    public static class Member
    {
        public static string Render(this Model.Public.Member member)
            => $"{ member.Name } = { member.Value },";
    }
}
