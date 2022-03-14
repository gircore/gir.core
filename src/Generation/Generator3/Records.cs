using System.Collections.Generic;
using Generator3.Generation.Record;
using Generator3.Publication;

namespace Generator3
{
    public static class Records
    {
        public static void Generate(this IEnumerable<GirModel.Record> records)
        {
            var internalMethodsGenerator = new InternalMethodsGenerator(
                template: new InternalMethodsTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var internalStructGenerator = new InternalStructGenerator(
                template: new InternalStructTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var internalStructDelegatesGenerator = new InternalDelegatesGenerator(
                template: new InternalDelegatesTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var internalSafeHandleGenerator = new InternalHandleGenerator(
                template: new InternalHandleTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var internalOwnedHandleGenerator = new InternalOwnedHandleGenerator(
                template: new InternalOwnedHandleTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var internalManagedHandleGenerator = new InternalManagedHandleGenerator(
                template: new InternalManagedHandleTemplate(),
                publisher: new InternalRecordFilePublisher()
            );

            var publicClassGenerator = new PublicClassGenerator(
                template: new PublicClassTemplate(),
                publisher: new PublicRecordFilePublisher()
            );

            foreach (var record in records)
            {
                internalMethodsGenerator.Generate(record);
                internalStructGenerator.Generate(record);
                internalStructDelegatesGenerator.Generate(record);
                internalSafeHandleGenerator.Generate(record);
                internalOwnedHandleGenerator.Generate(record);
                internalManagedHandleGenerator.Generate(record);

                publicClassGenerator.Generate(record);
            }
        }
    }
}
