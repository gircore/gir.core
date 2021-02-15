using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Repository;
using Repository.Analysis;
using Repository.Model;
using StrongInject;

namespace Generator
{
    public class Generator
    {
        private static string _cacheDir = "../gir-files";

        public readonly List<ILoadedProject> LoadedProjects;

        /// <summary>
        /// The main interface used to generate source files from GObject
        /// Introspection (GIR) data. Create a Generator object and then
        /// call <see cref="WriteAsync"/> in order to output generated code.
        /// </summary>
        /// <param name="projects"></param>
        public Generator(string[] projects)
        {
            // Repository does its own logging and error handling
            var repository = new Repository.Repository();
            LoadedProjects = repository.Load(ResolveFile, projects).ToList();

            try
            {
                Log.Information("Processing introspection data");
                
                // TODO: Do we want LoadedProject to contain an index of all symbols?
                // e.g. We could use linq queries on it to fetch certain symbols
                
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
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occured during processing. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
            }
            
            Log.Information("Ready to Write");
        }

        // TODO: Add more configuration options to Writer
        public int WriteAsync()
        {
            var writerService = new Container().Resolve().Value;
            
            //List<Task> asyncTasks = new();
            foreach (ILoadedProject proj in LoadedProjects)
            {
                writerService.Write(proj);
                //asyncTasks.Add(task);
            }

            try
            {
                //Task.WaitAll(asyncTasks.ToArray());
                Log.Information("Writing completed successfully");
                return 0;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occurred while writing files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
                return -1;
            }
        }
        
        private static FileInfo ResolveFile(string nspace, string version)
        {
            // Attempt to resolve dependencies
            
            // We store GIR files in the format 'Gtk-3.0.gir'
            // where 'Gtk' is the namespace and '3.0' the version
            var filename = $"{nspace}-{version}.gir";
            
            // Check current directory
            if (File.Exists(filename))
                return new FileInfo(filename);
            
            // Check cache directory
            var altPath = Path.Combine(_cacheDir, filename);
            if (File.Exists(altPath))
                return new FileInfo(altPath);
            
            // Fail
            throw new FileNotFoundException(
                $"Could not find file '{filename}' in the current directory or cache directory");
        }
    }
}
