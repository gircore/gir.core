using System.Collections.Generic;
using System.IO;
using Repository.Model;
using StrongInject;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string name, string version);

    public class Loader
    {
        public static IEnumerable<Namespace> Load(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            return new TargetsContainer().Run(targetsLoader => targetsLoader.GetNamespaces(fileFunc, targets));
        }
    }
}
