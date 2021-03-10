using System.Collections.Generic;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class ClassInstanceStructResolver
    {
        public void Resolve(IEnumerable<LoadedProject> projects)
        {
            foreach (var proj in projects)
            {
                var instanceStructs = CreateInstanceStruct(proj);
                ResolveInstanceStructs(instanceStructs);
                Log.Information($"Resolved class instance structs for {proj.Name}.");
            }
        }

        private IEnumerable<Record> CreateInstanceStruct(LoadedProject loadedProject)
        {
            var result = new List<Record>();
            
            foreach (var cls in loadedProject.Namespace.Classes)
            {
                Record instanceStruct = CreateInstanceRecord(
                    @namespace: loadedProject.Namespace,
                    fields: cls.Fields
                );
                
                cls.ClearFields();
                cls.Metadata["InstanceStruct"] = instanceStruct;
                
                result.Add(instanceStruct);
            }

            return result;
        }
        
        private void ResolveInstanceStructs(IEnumerable<Record> records)
        {
            foreach (var record in records)
                ResolveInstanceStructFields(record);
        }

        private void ResolveInstanceStructFields(Record instanceStruct)
        {
            foreach (var field in instanceStruct.Fields)
            {
                if (!field.SymbolReference.IsPointer && field.SymbolReference.GetSymbol() is Class cls)
                {
                    var foreignInstanceStruct = cls.Metadata["InstanceStruct"];
                    if (foreignInstanceStruct is Record r)
                    {
                        field.SymbolReference.ResolveAs(r);
                    }
                }
            }
        }
        
        private Record CreateInstanceRecord(Namespace @namespace, string nameprefix, IEnumerable<Field> fields) => new Record(
            @namespace: @namespace,
            name: "Instance",
            managedName: "Instance",
            gLibClassStructFor: null,
            methods: Enumerable.Empty<Method>(),
            functions: Enumerable.Empty<Method>(),
            getTypeFunction: null,
            fields: fields,
            disguised: false,
            constructors: Enumerable.Empty<Method>()
        );
    }
}
