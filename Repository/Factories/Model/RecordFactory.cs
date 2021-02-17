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
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IMethodFactory _methodFactory;

        public RecordFactory(SymbolReferenceFactory symbolReferenceFactory, IMethodFactory methodFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
        }

        public Record Create(RecordInfo @record, Namespace @namespace)
        {
            if (@record.Name is null)
                throw new Exception("Record is missing a name");
            
            return new Record(
                @namespace: @namespace, 
                nativeName: @record.Name, 
                managedName: @record.Name, 
                gLibClassStructFor: _symbolReferenceFactory.CreateWithNull(record.GLibIsGTypeStructFor, false),
                methods:_methodFactory.Create(@record.Methods, @namespace),
                functions: _methodFactory.Create(@record.Functions, @namespace)
            );
        }
    }
}
