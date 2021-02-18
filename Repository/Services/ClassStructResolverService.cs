using System.Collections.Generic;
using System.Linq;
using Repository.Model;

namespace Repository.Services
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
            foreach(var cls in classes)
            {
                var classStruct = FindClassStruct(classStructs, cls);

                if (classStruct is not null)
                {
                    classStruct.ManagedName = $"{cls.ManagedName}.Native.ClassStruct";
                    cls.ClassStruct = classStruct;
                    cls.Namespace.RemoveRecord(classStruct);
                }
            }
        }

        private static Record? FindClassStruct(IEnumerable<Record> classStructs, Class cls)
            => classStructs.FirstOrDefault(x => x.GLibClassStructFor!.GetSymbol() == cls);
    }
}
