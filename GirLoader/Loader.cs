using System.Collections.Generic;
using StrongInject;

namespace Gir
{
    public delegate File GetGirFile(Output.Model.Include include);

    [RegisterModule(typeof(Input.Module))]
    [RegisterModule(typeof(Output.Module))]
    public partial class Loader : IContainer<Output.Loader>
    {
        public static IEnumerable<Output.Model.Repository> Load(GetGirFile girFileFunc, IEnumerable<File> targets)
        {
            return new Loader(girFileFunc).Run(outputLoader => outputLoader.LoadRepositories(targets));
        }
        
        private readonly GetGirFile _getGirFile;

        internal Loader(GetGirFile getGirFile)
        {
            _getGirFile = getGirFile;
        }

        [Factory]
        internal GetGirFile GetGirFileDelegate() => _getGirFile;
    }
}
