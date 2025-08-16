using System;

namespace GirLoader.Output;

internal class InterfaceFactory
{
    private readonly TypeReferenceFactory _typeReferenceFactory;
    private readonly MethodFactory _methodFactory;
    private readonly PropertyFactory _propertyFactory;
    private readonly FunctionFactory _functionFactory;
    private readonly SignalFactory _signalFactory;

    public InterfaceFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FunctionFactory functionFactory, SignalFactory signalFactory)
    {
        _typeReferenceFactory = typeReferenceFactory;
        _methodFactory = methodFactory;
        _propertyFactory = propertyFactory;
        _functionFactory = functionFactory;
        _signalFactory = signalFactory;
    }

    public Interface Create(Input.Interface @interface, Repository repository)
    {
        if (@interface.Name is null)
            throw new Exception("Interface is missing a name");

        if (@interface.TypeName is null)
            throw new Exception($"Interface {@interface.Name} is missing a {nameof(@interface.TypeName)}");

        if (@interface.GetTypeFunction is null)
            throw new Exception($"Interface {@interface.Name} is missing a {nameof(@interface.GetTypeFunction)}");

        var iface = new Interface(
            repository: repository,
            cType: @interface.Type,
            name: @interface.Name,
            implements: _typeReferenceFactory.Create(@interface.Implements),
            methods: _methodFactory.Create(@interface.Methods),
            functions: _functionFactory.Create(@interface.Functions, repository),
            getTypeFunction: _functionFactory.CreateGetTypeFunction(@interface.GetTypeFunction, repository),
            properties: _propertyFactory.Create(@interface.Properties),
            signals: _signalFactory.Create(@interface.Signals),
            introspectable: @interface.Introspectable
        );

        iface.GetTypeFunction.SetParent(iface);

        if (iface.Constructors is not null)
            foreach (var constructor in iface.Constructors)
                constructor.SetParent(iface);

        foreach (var function in iface.Functions)
            function.SetParent(iface);

        foreach (var method in iface.Methods)
            method.SetParent(iface);

        return iface;
    }
}
