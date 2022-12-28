using System;
using OneOf.Types;

namespace GirLoader.Output;

internal class UnionFactory
{
    private readonly MethodFactory _methodFactory;
    private readonly FieldFactory _fieldFactory;
    private readonly ConstructorFactory _constructorFactory;
    private readonly FunctionFactory _functionFactory;

    public UnionFactory(MethodFactory methodFactory, FieldFactory fieldFactory, ConstructorFactory constructorFactory, FunctionFactory functionFactory)
    {
        _methodFactory = methodFactory;
        _fieldFactory = fieldFactory;
        _constructorFactory = constructorFactory;
        _functionFactory = functionFactory;
    }

    public Union Create(Input.Union union, Repository repository)
    {
        if (union.Name is null)
            throw new Exception("Union is missing a name");

        Function? getTypeFunction = union.GetTypeFunction switch
        {
            { } f => _functionFactory.CreateGetTypeFunction(f, repository),
            _ => null
        };

        var newUnion = new Union(
            repository: repository,
            cType: union.CType,
            name: union.Name,
            methods: _methodFactory.Create(union.Methods),
            functions: _functionFactory.Create(union.Functions, repository),
            getTypeFunction: getTypeFunction,
            fields: _fieldFactory.Create(union.Fields, repository),
            disguised: union.Disguised,
            constructors: _constructorFactory.Create(union.Constructors),
            introspectable: union.Introspectable
        );

        if (getTypeFunction is not null)
            getTypeFunction.SetParent(newUnion);

        foreach (var function in newUnion.Functions)
            function.SetParent(newUnion);

        return newUnion;
    }
}
