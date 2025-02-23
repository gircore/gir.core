namespace Generator.Model;

internal static partial class Member
{
    public static string GetName(GirModel.Member member)
    {
        return member.Name.ToPascalCase().EscapeIdentifier();
    }
}
