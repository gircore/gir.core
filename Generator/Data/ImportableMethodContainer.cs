using System.Collections.Generic;

namespace Generator
{
    public class ImportableMethodContainer
    {
        public string @Namespace { get; }
        public string Name { get; }
        public IEnumerable<ImportableMethod> Methods { get; }

        public ImportableMethodContainer(string @namespace, string name, IEnumerable<ImportableMethod> methods)
        {
            Namespace = @namespace ?? throw new System.ArgumentNullException(nameof(@namespace));
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Methods = methods ?? throw new System.ArgumentNullException(nameof(methods));
        }
    }
}