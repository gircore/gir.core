using Generator3.Renderer.Converter;

namespace Generator3.Model.Public
{
    public class Member
    {
        private readonly GirModel.Member _member;

        public string Name => _member.GetPublicName();
        public long Value => _member.Value;

        public Member(GirModel.Member member)
        {
            _member = member;
        }
    }
}
