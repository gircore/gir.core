namespace Repository.Model
{
    public class Member : Symbol
    {
        public string Value { get; }

        public Member(string nativeName, string managedName, string value) : base(nativeName, managedName)
        {
            Value = value;
        }
    }
}
