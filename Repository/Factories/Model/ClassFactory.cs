using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IClassFactory
    {
        Class Create(ClassInfo cls, Namespace @namespace);
    }

    public class ClassFactory : IClassFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly IMethodFactory _methodFactory;
        private readonly IPropertyFactory _propertyFactory;
        private readonly IFieldFactory _fieldFactory;
        private readonly ISignalFactory _signalFactory;

        public ClassFactory(ISymbolReferenceFactory symbolReferenceFactory, IMethodFactory methodFactory, IPropertyFactory propertyFactory, IFieldFactory fieldFactory, ISignalFactory signalFactory)
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
                implements: GetTypeReferences(cls.Implements),
                methods: _methodFactory.Create(cls.Methods, @namespace),
                functions: _methodFactory.Create(cls.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(cls.GetTypeFunction, @namespace),
                properties: _propertyFactory.Create(cls.Properties),
                fields: _fieldFactory.Create(cls.Fields),
                signals: _signalFactory.Create(cls.Signals)
            );
        }

        private IEnumerable<ISymbolReference> GetTypeReferences(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<ISymbolReference>();

            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(_symbolReferenceFactory.Create(implement.Name, false));
            }

            return list;
        }
    }
}
