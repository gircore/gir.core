using StrongInject;

namespace Gir
{
    [Register(typeof(FileLoader))]
    [RegisterModule(typeof(Input.Module))]
    [RegisterModule(typeof(Output.Module))]
    internal partial class FileLoaderContainer : IContainer<FileLoader>
    {
        private readonly GetGirFile _getGirFile;

        public FileLoaderContainer(GetGirFile getGirFile)
        {
            _getGirFile = getGirFile;
        }

        [Factory]
        public GetGirFile GetGirFileDelegate() => _getGirFile;
    }
}
