using System;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class RecordFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly FieldFactory _fieldFactory;

        public RecordFactory(SymbolReferenceFactory symbolReferenceFactory, MethodFactory methodFactory, FieldFactory fieldFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
            _fieldFactory = fieldFactory;
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
                name: @record.Name, 
                managedName: @record.Name, 
                gLibClassStructFor: GetGLibClassStructFor(@record.GLibIsGTypeStructFor, @namespace.Name),
                methods:_methodFactory.Create(@record.Methods, @namespace),
                functions: _methodFactory.Create(@record.Functions, @namespace),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(@record.Fields, @namespace),
                disguised: @record.Disguised,
                constructors: _methodFactory.Create(@record.Constructors, @namespace),
                ctype: @record.CType
            );
        }

        private SymbolReference? GetGLibClassStructFor(string? classStructForName, NamespaceName namespaceName)
        {
            SymbolReference? getGLibClassStructFor = null;
            
            if (classStructForName is {})
                getGLibClassStructFor = _symbolReferenceFactory.Create(classStructForName, null, namespaceName);

            return getGLibClassStructFor;
        }
    }
}
