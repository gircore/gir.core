using System.Collections.Generic;
using System.IO;
using StrongInject;

namespace Repository
{
    public delegate GirFile GetGirFile(Model.Include include);

    public class Loader
    {
        public static IEnumerable<Model.Repository> Load(GetGirFile girFileFunc, IEnumerable<GirFile> targets)
        {
            return new GirFileLoaderContainer(girFileFunc).Run(girFileLoader => girFileLoader.LoadRepositories(targets));
        }
    }
}
