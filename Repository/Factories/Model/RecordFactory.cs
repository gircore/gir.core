using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public class RecordFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly MethodFactory _methodFactory;

        public RecordFactory(SymbolReferenceFactory symbolReferenceFactory, MethodFactory methodFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
        }

        public Record Create(RecordInfo @record, Namespace @namespace)
        {
            if (@record.Name is null)
                throw new Exception("Record is missing a name");

            Method? getTypeFunction = @record.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f, @namespace),
                _ => null
            };
            
            return new Record(
                @namespace: @namespace, 
                nativeName: @record.Name, 
                managedName: @record.Name, 
                gLibClassStructFor: _symbolReferenceFactory.CreateWithNull(record.GLibIsGTypeStructFor, false),
                methods:_methodFactory.Create(@record.Methods, @namespace),
                functions: _methodFactory.Create(@record.Functions, @namespace),
                getTypeFunction: getTypeFunction
            );
        }
    }
}
