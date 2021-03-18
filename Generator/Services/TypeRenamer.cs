using System.Collections.Generic;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class TypeRenamer
    {
        public void SuffixDelegates(IEnumerable<LoadedProject> projects)
        {
            foreach (LoadedProject project in projects)
            {
                foreach (Callback dlg in project.Namespace.Callbacks)
                    dlg.SymbolName = new SymbolName(dlg.SymbolName + "Native");

                foreach (Callback dlg in GetFieldCallbacks(project))
                    dlg.SymbolName = new SymbolName(dlg.SymbolName + "Native");
            }

            Log.Information("Suffixed delegates.");
        }

        private IEnumerable<Callback> GetFieldCallbacks(LoadedProject project)
            => project.Namespace.Records.SelectMany(
                x => x.Fields.Select(y => y.Callback)).Where(x => x is { })!;

        public void SetClassStructMetadata(IEnumerable<LoadedProject> projects)
        {
            foreach (LoadedProject project in projects)
            {
                foreach (var record in project.Namespace.Records)
                {
                    if (record.GLibClassStructFor is { })
                    {
                        var className = record.GLibClassStructFor.GetSymbol().SymbolName;
                        record.Metadata["ClassName"] = className;
                        record.Metadata["PureName"] = "Class";
                        

                        record.SymbolName = new SymbolName($"{className}.Native.Class");
                    }
                }
            }
            
            Log.Information("Class struct metadata set.");
        }
    }
}
