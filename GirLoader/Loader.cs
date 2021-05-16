using System.Collections.Generic;
using StrongInject;

namespace GirLoader
{
    public delegate GirFile GetGirFile(Output.Model.Include include);

    [RegisterModule(typeof(Input.Module))]
    [RegisterModule(typeof(Output.Module))]
    public partial class Loader : IContainer<Output.Loader>
    {
        public static void EnableDebugOutput()
            => Log.EnableDebugOutput();

        public static void EnableVerboseOutput()
            => Log.EnableVerboseOutput();

        public static IEnumerable<Output.Model.Repository> Load(GetGirFile getGirFile, IEnumerable<GirFile> girFiles)
        {
            return new Loader(getGirFile).Run(outputLoader => outputLoader.LoadRepositories(girFiles));
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
