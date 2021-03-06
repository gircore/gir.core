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
                    dlg.NativeName += "Native";

                foreach (Callback dlg in GetFieldCallbacks(project))
                    dlg.NativeName += "Native";
            }

            Log.Information("Suffixed delegates.");
        }

        private IEnumerable<Callback> GetFieldCallbacks(LoadedProject project)
            => project.Namespace.Records.SelectMany(
                x => x.Fields.Select(y => y.Callback)).Where(x => x is { })!;

        public void SetNativeNames(IEnumerable<LoadedProject> projects)
        {
            foreach (LoadedProject project in projects)
            {
                // All structured data types are always expected to be handled
                // as a pointer if used as an argument or return value.
                // This is GObjec convention. If this changes it is expected
                // that an additional attribute is inserted in the gir file.

                var structuredTypes = IEnumerables.Concat<Symbol>(
                    project.Namespace.Unions,
                    project.Namespace.Records,
                    project.Namespace.Classes,
                    project.Namespace.Interfaces
                );

                foreach (var type in structuredTypes)
                    type.NativeName = "IntPtr";
            }

            Log.Information("Native names set.");
        }
    }
}
