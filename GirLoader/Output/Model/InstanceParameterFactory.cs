using System;
using GirLoader.Input.Model;

namespace GirLoader.Output.Model
{
    internal class InstanceParameterFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public InstanceParameterFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _transferFactory = transferFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public InstanceParameter Create(InstanceParameterInfo parameterInfo, NamespaceName namespaceName)
        {
            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");

            var name = Helper.String.EscapeIdentifier(parameterInfo.Name);

            return new InstanceParameter(
                originalName: new SymbolName(name),
                symbolName: new SymbolName(Helper.String.ToCamelCase(name)),
                typeReference: _typeReferenceFactory.Create(parameterInfo, namespaceName),
                direction: DirectionFactory.Create(parameterInfo.Direction),
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                callerAllocates: parameterInfo.CallerAllocates,
                typeInformation: _typeInformationFactory.Create(parameterInfo)
            );
        }
    }
}
