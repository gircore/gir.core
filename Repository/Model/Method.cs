namespace Repository.Model
{
    public class Method
    {
        public string Name { get; }
        public ReturnValue ReturnValue { get; }

        public Method(string name, ReturnValue returnValue)
        {
            Name = name;
            ReturnValue = returnValue;
        }
    }
}
