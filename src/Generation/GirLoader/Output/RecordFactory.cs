using System;
using System.Linq;

namespace GirLoader.Output;

internal class RecordFactory
{
    private readonly TypeReferenceFactory _typeReferenceFactory;
    private readonly MethodFactory _methodFactory;
    private readonly FieldFactory _fieldFactory;
    private readonly ConstructorFactory _constructorFactory;
    private readonly FunctionFactory _functionFactory;

    public RecordFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, FieldFactory fieldFactory, ConstructorFactory constructorFactory, FunctionFactory functionFactory)
    {
        _typeReferenceFactory = typeReferenceFactory;
        _methodFactory = methodFactory;
        _fieldFactory = fieldFactory;
        _constructorFactory = constructorFactory;
        _functionFactory = functionFactory;
    }

    public Record Create(Input.Record record, Repository repository)
    {
        if (record.Name is null)
            throw new Exception("Record is missing a name");

        Function? getTypeFunction = record.GetTypeFunction switch
        {
            { } f => _functionFactory.CreateGetTypeFunction(f, repository),
            _ => null
        };

        var methods = _methodFactory.Create(record.Methods);

        var newRecord = new Record(
            repository: repository,
            cType: record.CType,
            name: record.Name,
            gLibClassStructFor: GetGLibClassStructFor(record.GLibIsGTypeStructFor, repository.Namespace),
            methods: methods,
            functions: _functionFactory.Create(record.Functions, repository),
            getTypeFunction: getTypeFunction,
            fields: _fieldFactory.Create(record.Fields, repository),
            disguised: record.Disguised,
            constructors: _constructorFactory.Create(record.Constructors),
            introspectable: record.Introspectable,
            foreign: record.Foreign,
            opaque: record.Opaque,
            pointer: record.Pointer,
            copyFunction: methods.FirstOrDefault(x => x.Identifier == record.CopyFunction),
            freeFunction: methods.FirstOrDefault(x => x.Identifier == record.FreeFunction)
        );

        if (getTypeFunction is not null)
            getTypeFunction.SetParent(newRecord);

        foreach (var constructor in newRecord.Constructors)
            constructor.SetParent(newRecord);

        foreach (var function in newRecord.Functions)
            function.SetParent(newRecord);

        foreach (var method in newRecord.Methods)
            method.SetParent(newRecord);

        return newRecord;
    }

    private TypeReference? GetGLibClassStructFor(string? classStructForName, Namespace @namespace)
    {
        TypeReference? getGLibClassStructFor = null;

        if (classStructForName is { })
        {
            //We can generate the CType automatically because the class struct
            //of a class must be part of the repository of the class itself.
            var ctype = @namespace.IdentifierPrefixes + classStructForName;
            getGLibClassStructFor = _typeReferenceFactory.CreateResolveable(classStructForName, ctype);
        }

        return getGLibClassStructFor;
    }
}
