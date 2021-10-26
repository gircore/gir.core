using Generator3.Generation.Callback;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly NativeGenerator _nativeCallbackGenerator = new (
            renderer: new Renderer.NativeCallbackUnit(),
            publisher: new NativeDelegateFilePublisher()
        );
        
        public void GenerateCallback(string project, GirModel.Callback callback)
        {
            _nativeCallbackGenerator.Generate(project, callback);
        }
    }
}
