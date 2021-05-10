using System;

namespace Repository.Model
{
    internal class SingleParameterFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public SingleParameterFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }

        public SingleParameter Create(Xml.Parameter parameter, NamespaceName currentNamespace)
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

            var elementName = new ElementName(_identifierConverter.EscapeIdentifier(parameter.Name));
            var elementManagedName = new SymbolName(_identifierConverter.EscapeIdentifier(_caseConverter.ToCamelCase(parameter.Name)));

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
            public VarArgsNotSupportedException(string message) : base(message)
            {

            }
        }
    }
}
