using System.Collections.Generic;

namespace Generator
{
    public class ImportableMethod : Method
    {
        string Import { get; }
        string EntryPoint { get; }
        string? Summary{ get; }
        bool Obsolete { get; }
        string? ObsoleteSummary { get; }

        public ImportableMethod(string import, string entryPoint, bool obsolete, string name, string returnType, IEnumerable<Parameter> parameters) : base(name, returnType, parameters)
        {
            Import = import ?? throw new System.ArgumentNullException(nameof(import));
            EntryPoint = entryPoint ?? throw new System.ArgumentNullException(nameof(entryPoint));
            Obsolete = obsolete;
        }


    }
}