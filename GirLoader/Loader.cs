using System.Collections.Generic;
using System.IO;
using StrongInject;

namespace Gir
{
    public delegate File GetGirFile(Model.Include include);

    public class Loader
    {
        public static IEnumerable<Model.Repository> Load(GetGirFile girFileFunc, IEnumerable<File> targets)
        {
            return new FileLoaderContainer(girFileFunc).Run(girFileLoader => girFileLoader.LoadRepositories(targets));
        }
    }
}
