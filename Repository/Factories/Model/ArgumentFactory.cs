using System;
using Repository.Analysis;
using Repository.Factories.Model;
using Repository.Model;
using Repository.Services;
using Repository.Xml;
using Array = Repository.Model.Array;

namespace Repository.Factories
{
    internal class ArgumentFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public ArgumentFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }
        
        public Argument Create(ParameterInfo parameterInfo, NamespaceName currentNamespace)
        {
            if (parameterInfo.VarArgs is { })
                throw new VarArgsNotSupportedException("Arguments containing variadic paramters are not supported.");
            
            // Direction (for determining in/out/ref)
            var callerAllocates = parameterInfo.CallerAllocates;
            Direction direction = parameterInfo.Direction switch
            {
                "in" => Direction.In,
                "out" when callerAllocates => Direction.OutCallerAllocates,
                "out" when !callerAllocates => Direction.OutCalleeAllocates,
                "inout" => Direction.Ref,
                _ => Direction.Default
            };

            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");

            var elementName = new ElementName(_identifierConverter.Convert(parameterInfo.Name));
            var elementManagedName = new SymbolName(_caseConverter.ToCamelCase(_identifierConverter.Convert(parameterInfo.Name)));
            
            return new Argument(
                elementName: elementName,
                symbolName: elementManagedName,
                symbolReference: _symbolReferenceFactory.Create(parameterInfo, currentNamespace),
                direction: direction,
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                closureIndex: parameterInfo.Closure == -1 ? null : parameterInfo.Closure,
                destroyIndex: parameterInfo.Destroy == -1 ? null : parameterInfo.Destroy,
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
