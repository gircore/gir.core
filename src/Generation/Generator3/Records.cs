using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Record;

namespace Generator3
{
    public static class Records
    {
        public static void Generate(this IEnumerable<GirModel.Record> records)
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
            
            var nativeManagedHandleGenerator = new NativeManagedHandleGenerator(
                template: new NativeManagedHandleTemplate(),
                publisher: new NativeRecordFilePublisher()
            );

            var classGenerator = new ClassGenerator(
                template: new ClassTemplate(),
                publisher: new PublicRecordFilePublisher()
            );

            foreach (var record in records)
            {
                nativeMethodsGenerator.Generate(record);
                nativeStructGenerator.Generate(record);
                nativeStructDelegatesGenerator.Generate(record);
                nativeSafeHandleGenerator.Generate(record);
                nativeManagedHandleGenerator.Generate(record);

                classGenerator.Generate(record);
            }
        }
    }
}
