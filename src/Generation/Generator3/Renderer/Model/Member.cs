namespace Generator3.Renderer
{
    public static class Member
    {
        public static string Render(this Model.Member member)
            => $"{ member.Name } = { member.Value },";
    }
}
