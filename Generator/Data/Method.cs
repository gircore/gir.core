using System.Collections.Generic;

namespace Generator
{
    public class Method
    {
        string Name { get; }
        string ReturnType { get; }
        IEnumerable<Parameter> Parameters { get; }

        public Method(string name, string returnType, IEnumerable<Parameter> parameters)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            ReturnType = returnType ?? throw new System.ArgumentNullException(nameof(returnType));
            Parameters = parameters ?? throw new System.ArgumentNullException(nameof(parameters));
        }
    }
}