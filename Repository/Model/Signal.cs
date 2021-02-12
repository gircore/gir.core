using System.Collections.Generic;

namespace Repository.Model
{
    public class Signal : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }

        public Signal(string nativeName, string managedName, ReturnValue returnValue, IEnumerable<Argument> arguments) : base(nativeName, managedName)
        {
            ReturnValue = returnValue;
            Arguments = arguments;
        }

        public override string ToString()
            => ManagedName;
    }
}
