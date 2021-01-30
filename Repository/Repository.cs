using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string name, string version);
    
    public class Repository
    {
        public readonly List<LoadedProject> LoadedProjects;
        public readonly TypeDictionary TypeDict;
        
        public Repository(ResolveFileFunc fileFunc, string[] targets)
        {
            Log.Information($"Initialising generator with {targets.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            var loader = new Loader(targets, fileFunc, "../gir-files");
            
            // Sorts dependencies in order of base -> top level. Will throw if
            // a circular dependency is found.
            LoadedProjects = loader.GetOrderedList();
            
            // Resolve References
            // The NamespaceInfoConverterService creates unresolved references. We need to crawl through
            // project data and resolve each reference relative to the current
            // namespace.

            TypeDict = new TypeDictionary();

            foreach (LoadedProject proj in LoadedProjects)
            {
                Log.Information($"Analysing '{proj.ProjectName}'.");

                try
                {
                    Namespace nspace = proj.Namespace;

                    // Add Aliases
                    foreach (Alias alias in nspace.Aliases)
                        TypeDict.AddAlias(nspace.Name, alias.From, alias.To);
                
                    // Add Symbols
                    foreach (Class cls in nspace.Classes)
                        TypeDict.AddSymbol(nspace.Name, cls);

                    foreach (Interface iface in nspace.Interfaces)
                        TypeDict.AddSymbol(nspace.Name, iface);
                
                    foreach (Callback dlg in nspace.Callbacks)
                        TypeDict.AddSymbol(nspace.Name, dlg);
                
                    foreach (Enumeration @enum in nspace.Enumerations)
                        TypeDict.AddSymbol(nspace.Name, @enum);
                    
                    foreach (Enumeration bitfield in nspace.Bitfields)
                        TypeDict.AddSymbol(nspace.Name, bitfield);
                
                    foreach (Record @record in nspace.Records)
                        TypeDict.AddSymbol(nspace.Name, @record);
                    
                    // Resolve Symbols
                    Log.Information("Resolving symbol references.");

                    // A TypeDictionaryView allows us to query qualified and unqualified names
                    // from the perspective of a given namespace. Unqualified names are assumed
                    // to be defined in the current namespace. For project 'Gtk':
                    //  - 'Application'     resolves to 'Gtk.Application' (Internal)
                    //  - 'Gio.Application' resolves to 'Gio.Application' (External)
                    TypeDictionaryView view = TypeDict.GetView(nspace.Name);

                    // Resolve References
                    foreach (TypeReference reference in proj.UnresolvedReferences)
                    {
                        ISymbol symbol = view.LookupSymbol(reference.UnresolvedName);
                    
                        ReferenceType kind = (symbol?.Namespace?.Name == nspace.Name)
                            ? ReferenceType.Internal
                            : ReferenceType.External;
                    
                        reference.ResolveAs(symbol, kind);
                    }
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                    Log.Error($"Unable to analyse '{proj.ProjectName}'. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
                    return;
                }
            }
            
            Log.Information($"Repository initialised with {targets.Length} top-level project(s) and {LoadedProjects.Count - targets.Length} dependencies.");
        }
    }
}
