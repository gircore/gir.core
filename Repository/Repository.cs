using System.Collections.Generic;
using System.IO;
using Repository.Model;
using StrongInject;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string name, string version);
    
    public class Repository
    {
        public IEnumerable<Namespace> Load(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            //This just wraps the container to provide a nice public API.
            return new Container().Run(repository => repository.Load(fileFunc, targets));
        }
    }
}
