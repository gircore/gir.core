using System.Collections.Generic;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class ClassStructResolverService
    {
        public void Resolve(IEnumerable<LoadedProject> projects)
        {
            foreach (var proj in projects)
            {
                var classStructs = GetClassStructs(proj);
                UpdateClassesWithClassStructs(proj.Namespace.Classes, classStructs);

                Log.Information($"Resolved class structs for {proj.Name}.");
            }
        }

        private static IEnumerable<Record> GetClassStructs(LoadedProject loadedProject)
            => loadedProject.Namespace.Records.Where(x => (x.GLibClassStructFor is not null));

        private static void UpdateClassesWithClassStructs(IEnumerable<Class> classes, IEnumerable<Record> classStructs)
        {
            var structs = classStructs.ToList();
            foreach(var cls in classes)
            {
                cls.AddClassStructs(structs);
            }
        }
    }
}
