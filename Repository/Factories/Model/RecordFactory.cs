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
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;

        public RecordFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }

        public Record Create(RecordInfo @record, Namespace @namespace)
        {
            if (@record.Name is null)
                throw new Exception("Record is missing a name");
            
            return new Record(
                @namespace: @namespace, 
                nativeName: @record.Name, 
                managedName: @record.Name, 
                gLibClassStructFor: _symbolReferenceFactory.CreateWithNull(record.GLibIsGTypeStructFor, false)
            );
        }
    }
}
