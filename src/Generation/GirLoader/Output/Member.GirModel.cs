namespace GirLoader.Output
{
    public partial class Member : GirModel.Member
    {
        string GirModel.Member.Name => OriginalName;
        long GirModel.Member.Value => long.Parse(Value);
    }
}
