using System;

namespace GirLoader.Output
{
    internal class ClassFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly SignalFactory _signalFactory;
        private readonly ConstructorFactory _constructorFactory;
        private readonly FunctionFactory _functionFactory;

        public ClassFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FieldFactory fieldFactory, SignalFactory signalFactory, ConstructorFactory constructorFactory, FunctionFactory functionFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
            _fieldFactory = fieldFactory;
            _signalFactory = signalFactory;
            _constructorFactory = constructorFactory;
            _functionFactory = functionFactory;
        }

        public Class Create(Input.Class cls, Repository repository)
        {
            if (cls.Name is null)
                throw new Exception("Class is missing data");

            if (cls.GetTypeFunction is null)
                throw new Exception($"Class {cls.Name} is missing a get type function");

            return new Class(
                repository: repository,
                originalName: new TypeName(cls.Name),
                cType: cls.Type,
                parent: CreateParentTypeReference(cls.Parent, repository.Namespace),
                implements: _typeReferenceFactory.Create(cls.Implements),
                methods: _methodFactory.Create(cls.Methods),
                functions: _functionFactory.Create(cls.Functions, repository),
                getTypeFunction: _functionFactory.CreateGetTypeFunction(cls.GetTypeFunction, repository),
                properties: _propertyFactory.Create(cls.Properties),
                fields: _fieldFactory.Create(cls.Fields, repository),
                signals: _signalFactory.Create(cls.Signals),
                constructors: _constructorFactory.Create(cls.Constructors),
                isFundamental: cls.Fundamental
            );
        }

        private TypeReference? CreateParentTypeReference(string? parentName, Namespace @namespace)
        {
            if (parentName is { })
                return CreateTypeReference(parentName, @namespace);

            return null;
        }

        private TypeReference CreateTypeReference(string name, Namespace @namespace)
        {
            var ctype = !name.Contains(".") ? @namespace.IdentifierPrefixes + name : null;

            return _typeReferenceFactory.CreateResolveable(name, ctype);
        }
    }
}
