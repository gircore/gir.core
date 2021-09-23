namespace GirLoader.Output
{
    public partial class Member : GirModel.Member
    {
        string GirModel.Member.Name => Name;
        long GirModel.Member.Value => long.Parse(Value);
    }
}
