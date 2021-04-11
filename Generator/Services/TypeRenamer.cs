using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class TypeRenamer
    {
        public void FixPropertiesNamedLikeClass(IEnumerable<Namespace> namespaces)
        {
            foreach (var cls in namespaces.SelectMany(x => x.Classes))
                FixClassProperties(cls);
        }

        private static void FixClassProperties(Class cls)
        {
            foreach (var prop in cls.Properties)
            {
                if (prop.SymbolName == cls.SymbolName)
                {
                    prop.SymbolName = prop.SymbolName with { Value = prop.SymbolName + "Prop"};
                }
            }
        }

        public void SetMetadata(IEnumerable<Namespace> namespaces)
        {
            foreach (Namespace ns in namespaces)
            {
                SetRecordMetadata(ns.Records);
                SetRecordFieldsCallbackMetadata(ns.Records);
                SetUnionMetadata(ns.Unions);
                SetCallbacksMetadata(ns.Callbacks);
            }

            Log.Information("Metadata set.");
        }

        private void SetRecordFieldsCallbackMetadata(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                foreach (var callback in record.Fields.Select(x => x.Callback))
                {
                    if (callback is { })
                        SetCallbackMetadata(callback);
                }
            }
        }

        private void SetCallbacksMetadata(IEnumerable<Callback> callbacks)
        {
            foreach (var callback in callbacks)
                SetCallbackMetadata(callback);
        }

        private void SetCallbackMetadata(Callback callback)
        {
            callback.Metadata["ManagedName"] = callback.SymbolName;
            callback.SymbolName = new SymbolName(callback.SymbolName + "Callback");
        }

        private void SetUnionMetadata(IEnumerable<Union> unions)
        {
            foreach (var union in unions)
                SetUnionMetadata(union);
        }

        private void SetUnionMetadata(Union union)
        {
            union.Metadata["Name"] = union.SymbolName;
            union.Metadata["PureName"] = "Struct";

            union.SymbolName = new SymbolName($"{union.SymbolName}.Struct");
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
            record.Metadata["Name"] = className;
            record.Metadata["PureName"] = "Class";
            record.Metadata["SafeHandleName"] = "Handle";
            record.Metadata["SafeHandleRefName"] = $"{className}.Handle";

            record.SymbolName = new SymbolName($"{className}.Class");
        }

        private void SetRecordMetadata(Record record)
        {
            record.Metadata["Name"] = record.SymbolName;
            record.Metadata["PureName"] = "Struct";
            record.Metadata["SafeHandleName"] = "Handle";
            record.Metadata["SafeHandleRefName"] = $"{record.SymbolName}.Handle";

            record.SymbolName = new SymbolName($"{record.SymbolName}.Struct");
        }
    }
}
