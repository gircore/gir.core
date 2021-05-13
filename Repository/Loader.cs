using System.Collections.Generic;
using System.IO;
using StrongInject;

namespace Repository
{
    public delegate FileInfo GetFileInfo(Model.Include include);
    
    //TODO: Delete
    public delegate FileInfo ResolveFileFunc(string name, string version);

    public class Loader
    {
        public static IEnumerable<Model.Repository> Load(GetFileInfo fileFunc, IEnumerable<FileInfo> targets)
        {
            return new RepositoryLoaderContainer(fileFunc).Run(targetsLoader => targetsLoader.GetRepositories(targets));
        }
    }
}
