using Generator3.Converter;

namespace Generator3.Model.Public;

public record Member
{
    public string Name { get; }
    public long Value { get; }

    public Member(GirModel.Member member)
    {
        Name = member.GetPublicName();
        Value = member.Value;
    }
}
