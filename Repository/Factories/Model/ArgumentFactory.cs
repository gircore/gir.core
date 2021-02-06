using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IArgumentFactory
    {
        Argument Create(ParameterInfo parameterInfo);
    }

    public class ArgumentFactory : IArgumentFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;

        public ArgumentFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }
        
        public Argument Create(ParameterInfo parameterInfo)
        {
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

            // Memory Management information
            Transfer transfer = parameterInfo.TransferOwnership switch
            {
                "none" => Transfer.None,
                "container" => Transfer.Container,
                "full" => Transfer.Full,
                "floating" => Transfer.None,
                _ => Transfer.Full // TODO: Good default value? 
            };

            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");
            
            return new Argument(
                name: parameterInfo.Name,
                symbolReference: _symbolReferenceFactory.Create(parameterInfo),
                direction: direction,
                transfer: transfer,
                nullable: parameterInfo.Nullable
            );
        }
    }
}
