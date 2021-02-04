using System.Collections.Generic;

namespace Repository.Model
{
    public class Method
    {
        public string Name { get; }
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }

        public Method(string name, ReturnValue returnValue, IEnumerable<Argument> arguments)
        {
            Name = name;
            ReturnValue = returnValue;
            Arguments = arguments;
        }
    }
}
