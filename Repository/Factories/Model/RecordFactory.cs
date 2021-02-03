using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IRecordFactory
    {
        Record Create(RecordInfo @record, Namespace @namespace);
    }

    public class RecordFactory : IRecordFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;

        public RecordFactory(ITypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Record Create(RecordInfo @record, Namespace @namespace)
        {
            if (@record.Name is null)
                throw new Exception("Record is missing a name");
            
            return new Record(
                @namespace: @namespace, 
                nativeName: @record.Name, 
                managedName: @record.Name, 
                gLibClassStructFor: _typeReferenceFactory.CreateWithNull(record.GLibIsGTypeStructFor, false)
            );
        }
    }
}
