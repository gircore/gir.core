using System;
using Repository.Analysis;
using Repository.Factories.Model;
using Repository.Model;
using Repository.Services;
using Repository.Xml;
using Array = Repository.Model.Array;

namespace Repository.Factories
{
    internal class SingleParameterFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public SingleParameterFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }
        
        public SingleParameter Create(ParameterInfo parameterInfo, NamespaceName currentNamespace)
        {
            if (parameterInfo.VarArgs is { })
                throw new VarArgsNotSupportedException("Arguments containing variadic paramters are not supported.");

            Scope callbackScope = parameterInfo.Scope switch
            {
                "async" => Scope.Async,
                "notified" => Scope.Notified,
                _ => Scope.Call,
            };

            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");

            var elementName = new ElementName(_identifierConverter.Convert(parameterInfo.Name));
            var elementManagedName = new SymbolName(_identifierConverter.Convert(_caseConverter.ToCamelCase(parameterInfo.Name)));

            return new SingleParameter(
                elementName: elementName,
                symbolName: elementManagedName,
                symbolReference: _symbolReferenceFactory.Create(parameterInfo, currentNamespace),
                direction: DirectionFactory.Create(parameterInfo.Direction),
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                callerAllocates: parameterInfo.CallerAllocates,
                closureIndex: parameterInfo.Closure == -1 ? null : parameterInfo.Closure,
                destroyIndex: parameterInfo.Destroy == -1 ? null : parameterInfo.Destroy,
                scope: callbackScope,
                typeInformation: _typeInformationFactory.Create(parameterInfo)
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
