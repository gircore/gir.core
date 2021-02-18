using System.Linq;
using Repository;
using StrongInject;

namespace Generator
{
    public class Generator
    {
        public void WriteAsync(string[] projects)
        {
            // Repository does its own logging and error handling
            var repository = new Repository.Repository();
            var loadedProjects = repository.Load(FileResolver.ResolveFile, projects).ToList();

            Log.Information("Processing introspection data");

            // Process Data
            /*
                foreach (LoadedProject proj in LoadedProjects)
                {
                    // Prefix Interfaces with 'I'
                    foreach (Interface iface in proj.Namespace.Interfaces)
                        iface.ManagedName = 'I' + iface.ManagedName;
                    
                    // Reparent Object Class Structs
                    List<Record> records = proj.Namespace.Records;
                    foreach (Record classStruct in records.Where(record => record.GLibClassStructFor != null))
                    {
                        IType type = classStruct.GLibClassStructFor!.Type;
                        if (type is not Class)
                            continue;
                        
                        classStruct.ManagedName = $"{type.ManagedName}.{classStruct.ManagedName}";
                        type.AddMetadata("ClassStruct", classStruct);
                    }
                }*/

            Log.Information("Ready to Write");

            var writerService = new Container().Resolve().Value;
            foreach (LoadedProject proj in loadedProjects)
            {
                writerService.Write(proj);
            }

            Log.Information("Writing completed successfully");
        }
    }
}
