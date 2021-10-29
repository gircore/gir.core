using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Record;

namespace Generator3
{
    public static class Records
    {
        public static void Generate(this IEnumerable<GirModel.Record> records, string project)
        {
            var nativeMethodsGenerator = new NativeMethodsGenerator(
                template: new NativeMethodsTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var nativeStructGenerator = new NativeStructGenerator (
                template: new NativeStructTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var nativeSafeHandleGenerator = new NativeSafeHandleGenerator(
                template: new NativeSafeHandleTemplate(),
                publisher: new NativeRecordFilePublisher()
            );
            
            foreach (var record in records)
            {
                nativeMethodsGenerator.Generate(project, @record);
                nativeStructGenerator.Generate(project, @record);
                nativeSafeHandleGenerator.Generate(project, @record);
            }
        }
    }
}
