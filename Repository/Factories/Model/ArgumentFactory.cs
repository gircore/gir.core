using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IArgumentFactory
    {
        Argument Create(ParameterInfo parameterInfo);
        Argument Create(string name, string type, bool isArray, Direction direction, Transfer transfer, bool nullable);
    }

    public class ArgumentFactory : IArgumentFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly ITransferFactory _transferFactory;
        private readonly IIdentifierConverter _identifierConverter;

        public ArgumentFactory(ISymbolReferenceFactory symbolReferenceFactory, ITransferFactory transferFactory, IIdentifierConverter identifierConverter)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
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
                name: _identifierConverter.Convert(parameterInfo.Name),
                symbolReference: _symbolReferenceFactory.Create(parameterInfo),
                direction: direction,
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable
            );
        }

        public Argument Create(string name, string type, bool isArray, Direction direction, Transfer transfer, bool nullable)
        {
            return new Argument(
                name: name,
                symbolReference: _symbolReferenceFactory.Create(type, isArray),
                direction: direction,
                transfer: transfer,
                nullable: nullable
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
