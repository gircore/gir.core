using System;

namespace GirLoader.Output;

internal class ClassFactory
{
    private readonly TypeReferenceFactory _typeReferenceFactory;
    private readonly MethodFactory _methodFactory;
    private readonly PropertyFactory _propertyFactory;
    private readonly FieldFactory _fieldFactory;
    private readonly SignalFactory _signalFactory;
    private readonly ConstructorFactory _constructorFactory;
    private readonly FunctionFactory _functionFactory;
    private readonly CallbackFactory _callbackFactory;

    public ClassFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FieldFactory fieldFactory, SignalFactory signalFactory, ConstructorFactory constructorFactory, FunctionFactory functionFactory, CallbackFactory callbackFactory)
    {
        _typeReferenceFactory = typeReferenceFactory;
        _methodFactory = methodFactory;
        _propertyFactory = propertyFactory;
        _fieldFactory = fieldFactory;
        _signalFactory = signalFactory;
        _constructorFactory = constructorFactory;
        _functionFactory = functionFactory;
        _callbackFactory = callbackFactory;
    }

    public Class Create(Input.Class cls, Repository repository)
    {
        if (cls.Name is null)
            throw new Exception("Class is missing data");

        if (cls.GetTypeFunction is null)
            throw new Exception($"Class {cls.Name} is missing a get type function");

        var @class = new Class(
            repository: repository,
            name: cls.Name,
            cType: cls.Type,
            typeName: cls.TypeName,
            parent: CreateParentTypeReference(cls.Parent, repository.Namespace),
            implements: _typeReferenceFactory.Create(cls.Implements),
            methods: _methodFactory.Create(cls.Methods),
            functions: _functionFactory.Create(cls.Functions, repository),
            getTypeFunction: _functionFactory.CreateGetTypeFunction(cls.GetTypeFunction, repository),
            properties: _propertyFactory.Create(cls.Properties),
            fields: _fieldFactory.Create(cls.Fields, repository),
            signals: _signalFactory.Create(cls.Signals),
            constructors: _constructorFactory.Create(cls.Constructors),
            callbacks: _callbackFactory.Create(cls.Callbacks, repository),
            fundamental: cls.Fundamental,
            @abstract: cls.Abstract,
            final: cls.Final,
            introspectable: cls.Introspectable
        );

        @class.GetTypeFunction.SetParent(@class);

        foreach (var constructor in @class.Constructors)
            constructor.SetParent(@class);

        foreach (var function in @class.Functions)
            function.SetParent(@class);

        foreach (var method in @class.Methods)
            method.SetParent(@class);

        foreach (var callback in @class.Callbacks)
            callback.SetParent(@class);

        return @class;
    }

    private TypeReference? CreateParentTypeReference(string? parentName, Namespace @namespace)
    {
        return parentName is not null
            ? CreateTypeReference(parentName, @namespace)
            : null;
    }

    private TypeReference CreateTypeReference(string name, Namespace @namespace)
    {
        var ctype = !name.Contains(".") ? @namespace.IdentifierPrefixes + name : null;

        return _typeReferenceFactory.CreateResolveable(name, ctype);
    }
}
