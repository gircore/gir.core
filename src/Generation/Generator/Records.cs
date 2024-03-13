using System.Collections.Generic;
using System.Linq;
using Generator.Generator;
using Generator.Model;

namespace Generator;

public static class Records
{
    public static void Generate(IEnumerable<GirModel.Record> records, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Record>>()
        {
            //Untyped records
            new Generator.Internal.UntypedRecord(publisher),
            new Generator.Internal.UntypedRecordData(publisher),
            new Generator.Internal.UntypedRecordHandle(publisher),
            new Generator.Public.UntypedRecord(publisher),
            
            //Foreign typed records
            new Generator.Internal.ForeignTypedRecord(publisher),
            new Generator.Internal.ForeignTypedRecordHandle(publisher),
            new Generator.Public.ForeignTypedRecord(publisher),
            
            //Opaque typed records
            new Generator.Internal.OpaqueTypedRecord(publisher),
            new Generator.Internal.OpaqueTypedRecordHandle(publisher),
            new Generator.Public.OpaqueTypedRecord(publisher),
            
            //Opaque untyped records
            new Generator.Internal.OpaqueUntypedRecord(publisher),
            new Generator.Internal.OpaqueUntypedRecordHandle(publisher),
            new Generator.Public.OpaqueUntypedRecord(publisher),
            
            //Typed records
            new Generator.Internal.TypedRecord(publisher),
            new Generator.Internal.TypedRecordDelegates(publisher),
            new Generator.Internal.TypedRecordHandle(publisher),
            new Generator.Internal.TypedRecordData(publisher),
            new Generator.Public.TypedRecord(publisher),
            
            //Regular records
            new Generator.Internal.RecordDelegates(publisher),
            new Generator.Internal.RecordHandle(publisher),
            new Generator.Internal.RecordOwnedHandle(publisher),
            new Generator.Internal.RecordMethods(publisher),
            new Generator.Internal.RecordStruct(publisher),
            new Generator.Internal.RecordManagedHandle(publisher),
            new Generator.Public.RecordClass(publisher)
        };

        foreach (var record in records.Where(Record.IsEnabled))
            foreach (var generator in generators)
                generator.Generate(record);
    }
}
