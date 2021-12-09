namespace Generator3.Renderer.Converter
{
    public static class MemberNameConverter
    {
        public static string GetPublicName(this GirModel.Member member)
        {
            return member.Name.ToPascalCase().EscapeIdentifier();
        }
    }
}
