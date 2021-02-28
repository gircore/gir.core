using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class ArgumentFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;

        public ArgumentFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
        }
        
        public Argument Create(ParameterInfo parameterInfo)
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

            return new Argument(
                nativeName: _identifierConverter.Convert(parameterInfo.Name),
                managedName: _caseConverter.ToCamelCase(_identifierConverter.Convert(parameterInfo.Name)),
                symbolReference: _symbolReferenceFactory.Create(parameterInfo),
                direction: direction,
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                closureIndex: parameterInfo.Closure == -1 ? null : parameterInfo.Closure,
                destroyIndex: parameterInfo.Destroy == -1 ? null : parameterInfo.Destroy
            );
        }

        public Argument Create(string nativeName, string type, bool isArray, Direction direction, Transfer transfer, bool nullable, int? closure, int? destroy)
        {
            return new Argument(
                nativeName: nativeName,
                managedName: _caseConverter.ToCamelCase(nativeName),
                symbolReference: _symbolReferenceFactory.Create(type, isArray),
                direction: direction,
                transfer: transfer,
                nullable: nullable,
                closureIndex: closure,
                destroyIndex: destroy
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
