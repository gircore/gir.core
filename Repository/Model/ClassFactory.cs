using System;
using Repository.Xml;

namespace Repository.Model
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
            if (cls.Name is null)
                throw new Exception("Class is missing data");

            if (cls.GetTypeFunction is null)
                throw new Exception($"Class {cls.Name} is missing a get type function");

            CTypeName? cTypeName = null;
            if (cls.Type is { })
                cTypeName = new CTypeName(cls.Type);

            return new Class(
                @namespace: @namespace,
                typeName: new TypeName(cls.Name),
                symbolName: new SymbolName(cls.Name),
                cTypeName: cTypeName,
                parent: GetParent(cls.Parent, @namespace.Name),
                implements: _symbolReferenceFactory.Create(cls.Implements, @namespace.Name),
                methods: _methodFactory.Create(cls.Methods, @namespace),
                functions: _methodFactory.Create(cls.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(cls.GetTypeFunction),
                properties: _propertyFactory.Create(cls.Properties, @namespace.Name),
                fields: _fieldFactory.Create(cls.Fields, @namespace),
                signals: _signalFactory.Create(cls.Signals, @namespace.Name),
                constructors: _methodFactory.Create(cls.Constructors, @namespace),
                isFundamental: cls.Fundamental
            );
        }

        private SymbolReference? GetParent(string? parentName, NamespaceName currentNamespace)
        {
            SymbolReference? parent = null;

            if (parentName is { })
                parent = _symbolReferenceFactory.Create(parentName, null, currentNamespace);

            return parent;
        }
    }
}
