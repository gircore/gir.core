namespace Repository.Model
{
    public class Signal : Symbol
    {
        public ReturnValue ReturnValue { get; }

        public Signal(string nativeName, string managedName, ReturnValue returnValue) : base(nativeName, managedName)
        {
            ReturnValue = returnValue;
        }

        public override string ToString()
            => ManagedName;
    }
}
