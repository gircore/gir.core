using System;

namespace GirLoader.Output;

internal class SingleParameterFactory
{
    private readonly TypeReferenceFactory _typeReferenceFactory;
    private readonly TransferFactory _transferFactory;

    public SingleParameterFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory)
    {
        _typeReferenceFactory = typeReferenceFactory;
        _transferFactory = transferFactory;
    }

    public SingleParameter Create(Input.Parameter parameter)
    {
        Scope? callbackScope = parameter.Scope switch
        {
            "async" => Scope.Async,
            "notified" => Scope.Notified,
            "call" => Scope.Call,
            "forever" => Scope.Forever,
            _ => null
        };

        if (parameter.Name is null)
            throw new Exception("Argument name is null");

        return new SingleParameter(
            name: parameter.Name,
            typeReferenceOrVarArgs: parameter.VarArgs is not null ? new VarArgs() : _typeReferenceFactory.Create(parameter),
            direction: DirectionFactory.Create(parameter.Direction),
            transfer: _transferFactory.FromText(parameter.TransferOwnership),
            nullable: parameter.Nullable,
            optional: parameter.Optional,
            callerAllocates: parameter.CallerAllocates,
            closureIndex: parameter.Closure == -1 ? null : parameter.Closure,
            destroyIndex: parameter.Destroy == -1 ? null : parameter.Destroy,
            scope: callbackScope
        );
    }
}
