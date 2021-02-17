using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IArgumentFactory
    {
        Argument Create(ParameterInfo parameterInfo);
        Argument Create(string name, string type, bool isArray, Direction direction, Transfer transfer, bool nullable, int closure, int destroy);
    }

    public class ArgumentFactory : IArgumentFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly ITransferFactory _transferFactory;
        private readonly IIdentifierConverter _identifierConverter;
        private readonly ICaseConverter _caseConverter;

        public ArgumentFactory(ISymbolReferenceFactory symbolReferenceFactory, ITransferFactory transferFactory, IIdentifierConverter identifierConverter, ICaseConverter caseConverter)
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
                closureIndex: parameterInfo.Closure,
                destroyIndex: parameterInfo.Destroy
            );
        }

        public Argument Create(string nativeName, string type, bool isArray, Direction direction, Transfer transfer, bool nullable, int closure, int destroy)
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
