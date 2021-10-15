using Generator3.Generation.Record;
using Generator3.Publication.Filesystem;

namespace Generator3
{
    public partial class Generator3
    {
        private readonly NativeFunctionsGenerator _recordNativeFunctionsGenerator = new (
            renderer: new Renderer.NativeRecordFunctionsUnit(),
            publisher: new NativeRecordFilePublisher()
        );
        
        private readonly NativeStructGenerator _recordNativeStructGenerator = new (
            renderer: new Renderer.NativeRecordStuctUnit(),
            publisher: new NativeRecordFilePublisher()
        );

        public void GenerateRecord(string project, GirModel.Record record)
        {
            _recordNativeFunctionsGenerator.Generate(project, record);
            _recordNativeStructGenerator.Generate(project, record);
        }
    }
}
