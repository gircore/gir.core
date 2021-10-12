namespace Generator3
{
    public partial class Generator3
    {
        private Generation.Unit.Native.RecordFunctions.Generator? _nativeRecordFunctionsGenerator;

        private Generation.Unit.Native.RecordFunctions.Generator NativeRecordFunctionsGenerator
            => _nativeRecordFunctionsGenerator ??= new Generation.Unit.Native.RecordFunctions.Generator(new Rendering.Templates.NativeRecordFunctions());

        private Publication.NativeRecordFunctionsPublisher NativeRecordFunctionsPublisher => _publisher;

        public void GenerateRecord(string project, GirModel.Record record)
        {
            var source = NativeRecordFunctionsGenerator.Generate(record);
            var codeUnit = new Publication.CodeUnit(project, $"{record.Name}.Functions", source);
            NativeRecordFunctionsPublisher.Publish(codeUnit);
        }
    }
}
