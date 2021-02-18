using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class ClassFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly SignalFactory _signalFactory;

        public ClassFactory(SymbolReferenceFactory symbolReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FieldFactory fieldFactory, SignalFactory signalFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
            _fieldFactory = fieldFactory;
            _signalFactory = signalFactory;
        }

        public Class Create(ClassInfo cls, Namespace @namespace)
        {
            if (cls.Name is null || cls.TypeName is null)
                throw new Exception("Class is missing data");

            if (cls.GetTypeFunction is null)
                throw new Exception($"Class {cls.Name} is missing a get type function");
            
            return new Class(
                @namespace: @namespace,
                nativeName: cls.Name,
                managedName: cls.Name,
                ctype: cls.TypeName,
                parent: _symbolReferenceFactory.CreateWithNull(cls.Parent, false),
                implements: _symbolReferenceFactory.Create(cls.Implements),
                methods: _methodFactory.Create(cls.Methods, @namespace),
                functions: _methodFactory.Create(cls.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(cls.GetTypeFunction, @namespace),
                properties: _propertyFactory.Create(cls.Properties),
                fields: _fieldFactory.Create(cls.Fields),
                signals: _signalFactory.Create(cls.Signals)
            );
        }
    }
}
