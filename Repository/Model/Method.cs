using System.Collections.Generic;

namespace Repository.Model
{
    public class Method : Type
    {
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }

        public Method(Namespace @namespace, string nativeName, string managedName, ReturnValue returnValue, IEnumerable<Argument> arguments) : base(@namespace, nativeName, managedName)
        {
            ReturnValue = returnValue;
            Arguments = arguments;
        }

        public override string ToString()
            => ManagedName;
    }
}
