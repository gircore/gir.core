namespace Generator3.Converter
{
    public static class MemberNameConverter
    {
        public static string GetPublicName(this GirModel.Member member)
        {
            return member.Name.ToPascalCase().EscapeIdentifier();
        }
    }
}
