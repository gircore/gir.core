using System.Collections.Generic;
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
                // We want to append 'Native' to the managed name property
                // of each delegate so that parameters referencing this
                // use the "native" version, instead of the user facing
                // "managed" version. See delegate.sbntxt for more.
                foreach (Callback dlg in project.Namespace.Callbacks)
                {
                    dlg.NativeName += "Native";
                }
            }

            Log.Information("Suffixed delegates.");
        }

        public void SetNativeNames(IEnumerable<LoadedProject> projects)
        {
            foreach (LoadedProject project in projects)
            {
                // All structured data types are always expected to be handled
                // as a pointer if used as an argument or return value.
                // This is GObjec convention. If this changes it is expected
                // that an additional attribute is inserted in the gir file.

                foreach (var record in project.Namespace.Records)
                    record.NativeName = "IntPtr";

                //TODO: rename native names for other structured data types
            }
            
            Log.Information("Native names set.");
        }
    }
}
