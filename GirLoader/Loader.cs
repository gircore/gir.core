using System.Collections.Generic;
using System.IO;
using StrongInject;

namespace Gir
{
    public delegate File GetGirFile(Output.Model.Include include);

    public class Loader
    {
        public static IEnumerable<Output.Model.Repository> Load(GetGirFile girFileFunc, IEnumerable<File> targets)
        {
            return new FileLoaderContainer(girFileFunc).Run(girFileLoader => girFileLoader.LoadRepositories(targets));
        }
    }
}
