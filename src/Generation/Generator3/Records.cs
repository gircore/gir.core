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
            
            var nativeStructDelegatesGenerator = new NativeDelegatesGenerator(
                template: new NativeDelegatesTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var nativeSafeHandleGenerator = new NativeSafeHandleGenerator(
                template: new NativeSafeHandleTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var classGenerator = new ClassGenerator(
                template: new ClassTemplate(),
                publisher: new RecordFilePublisher()
            );

            foreach (var record in records)
            {
                nativeMethodsGenerator.Generate(project, @record);
                nativeStructGenerator.Generate(project, @record);
                nativeStructDelegatesGenerator.Generate(project, @record);
                nativeSafeHandleGenerator.Generate(project, @record);

                classGenerator.Generate(project, @record);
            }
        }
    }
}
