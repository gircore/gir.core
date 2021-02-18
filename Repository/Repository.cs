using System.Collections.Generic;
using System.IO;
using StrongInject;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string name, string version);
    
    public class Repository
    {
        public IEnumerable<LoadedProject> Load(ResolveFileFunc fileFunc, string[] targets)
        {
            //This just wraps the container to provide a nice public API.
            return new Container().Run(repository => repository.Load(fileFunc, targets));
        }
    }
}
