using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Record;

namespace Generator3
{
    public static class Records
    {
        public static void Generate(this IEnumerable<GirModel.Record> records, string project)
        {
            var nativeFunctionsGenerator = new NativeFunctionsGenerator(
                template: new NativeFunctionsTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var nativeStructGenerator = new NativeStructGenerator (
                template: new NativeStructTemplate(),
                publisher: new NativeRecordFilePublisher()
            );
            
            foreach (var record in records)
            {
                nativeFunctionsGenerator.Generate(project, @record);
                nativeStructGenerator.Generate(project, @record);
            }
        }
    }
}
