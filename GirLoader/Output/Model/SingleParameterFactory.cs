using System;

namespace GirLoader.Output.Model
{
    internal class SingleParameterFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public SingleParameterFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _transferFactory = transferFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public SingleParameter Create(Input.Model.Parameter parameter, NamespaceName currentNamespace)
        {
            if (parameter.VarArgs is { })
                throw new VarArgsNotSupportedException("Arguments containing variadic paramters are not supported.");

            Scope callbackScope = parameter.Scope switch
            {
                "async" => Scope.Async,
                "notified" => Scope.Notified,
                _ => Scope.Call,
            };

            if (parameter.Name is null)
                throw new Exception("Argument name is null");

            var elementName = new ElementName(Helper.String.EscapeIdentifier(parameter.Name));
            var elementManagedName = new SymbolName(Helper.String.EscapeIdentifier(Helper.String.ToCamelCase(parameter.Name)));

            return new SingleParameter(
                elementName: elementName,
                symbolName: elementManagedName,
                typeReference: _typeReferenceFactory.Create(parameter, currentNamespace),
                direction: DirectionFactory.Create(parameter.Direction),
                transfer: _transferFactory.FromText(parameter.TransferOwnership),
                nullable: parameter.Nullable,
                callerAllocates: parameter.CallerAllocates,
                closureIndex: parameter.Closure == -1 ? null : parameter.Closure,
                destroyIndex: parameter.Destroy == -1 ? null : parameter.Destroy,
                scope: callbackScope,
                typeInformation: _typeInformationFactory.Create(parameter)
            );
        }

        public class VarArgsNotSupportedException : Exception
        {
            public VarArgsNotSupportedException(string message) : base(message) { }
        }
    }
}
