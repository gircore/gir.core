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
        }
    }
}
