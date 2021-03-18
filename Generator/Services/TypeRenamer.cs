using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class TypeRenamer
    {
        public void SetMetadata(IEnumerable<LoadedProject> loadedProjects)
        {
            foreach (LoadedProject project in loadedProjects)
            {
                SetRecordMetadata(project.Namespace.Records);
                SetUnionMetadata(project.Namespace.Unions);
            }
            
            Log.Information("Metadata set.");
        }

        private void SetUnionMetadata(IEnumerable<Union> unions)
        {
            foreach(var union in unions)
                SetUnionMetadata(union);
        }
        
        private void SetUnionMetadata(Union union)
        {
            union.Metadata["RecordName"] = union.SymbolName;
            union.Metadata["PureName"] = "Struct";

            union.SymbolName = new SymbolName($"{union.SymbolName}.Native.Struct");
        }
        
        private void SetRecordMetadata(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                if (record.GLibClassStructFor is { })
                    SetClassStructMetadata(record);
                else
                    SetRecordMetadata(record);
            }
        }

        private void SetClassStructMetadata(Record record)
        {
            Debug.Assert(record.GLibClassStructFor is not null);
            
            var className = record.GLibClassStructFor.GetSymbol().SymbolName;
            record.Metadata["ClassName"] = className;
            record.Metadata["PureName"] = "Class";

            record.SymbolName = new SymbolName($"{className}.Native.Class");
        }

        private void SetRecordMetadata(Record record)
        {
            record.Metadata["RecordName"] = record.SymbolName;
            record.Metadata["PureName"] = "Struct";

            record.SymbolName = new SymbolName($"{record.SymbolName}.Native.Struct");
        }
    }
}
