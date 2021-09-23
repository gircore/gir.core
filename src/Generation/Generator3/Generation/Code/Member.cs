namespace Generator3.Generation
{
    public class Member
    {
        private readonly GirModel.Member _member;

        public string Name => _member.Name;
        public long Value => _member.Value;
        public string Code => $"{ Name } = { Value },";

        public Member(GirModel.Member member)
        {
            _member = member;
        }
    }
}
