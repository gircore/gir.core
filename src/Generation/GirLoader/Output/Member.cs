namespace GirLoader.Output
{
    public partial class Member : Symbol
    {
        public string Value { get; }

        public Member(string originalName, string value) : base(originalName)
        {
            Value = value;
        }
    }
}
