using System;
using Repository.Xml;

namespace Repository.Model
{
    internal class InstanceParameterFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public InstanceParameterFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }

        public InstanceParameter Create(InstanceParameterInfo parameterInfo, NamespaceName currentNamespace)
        {
            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");

            var elementName = new ElementName(_identifierConverter.EscapeIdentifier(parameterInfo.Name));
            var elementManagedName = new SymbolName(_identifierConverter.EscapeIdentifier(_caseConverter.ToCamelCase(parameterInfo.Name)));

            return new InstanceParameter(
                elementName: elementName,
                symbolName: elementManagedName,
                typeReference: _typeReferenceFactory.Create(parameterInfo, currentNamespace),
                direction: DirectionFactory.Create(parameterInfo.Direction),
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                callerAllocates: parameterInfo.CallerAllocates,
                typeInformation: _typeInformationFactory.Create(parameterInfo)
            );
        }
    }
}
