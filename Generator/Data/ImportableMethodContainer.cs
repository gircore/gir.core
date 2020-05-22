using System.Collections.Generic;

namespace Generator
{
    public class ImportableMethodContainer
    {
        string @Namespace { get; }
        string Name { get; }
        IEnumerable<ImportableMethod> Methods { get; }

        public ImportableMethodContainer(string @namespace, string name, IEnumerable<ImportableMethod> methods)
        {
            Namespace = @namespace ?? throw new System.ArgumentNullException(nameof(@namespace));
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Methods = methods ?? throw new System.ArgumentNullException(nameof(methods));
        }
    }
}